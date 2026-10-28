using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StatType { Atk, Speed, CritChance, CritAtk, Resistance }

public class UpgradeOption
{
    public string title;
    public string desc;
    public Action apply;
}

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] PopupUI upgradeUI;
    [SerializeField] Button[] btns = new Button[3];

    [Header("스탯 1회 증가량")]
    [SerializeField] float atkUp = 10f;
    [SerializeField] float speedUp = 0.5f;
    [SerializeField] float critChanceUp = 0.05f;   // 확률 단위(0~1인지 0~100인지 맞추십시오)
    [SerializeField] float critAtkUp = 0.1f;
    [SerializeField] float resistanceUp = 0.05f;

    [Header("종류별 가중치 (비율)")]
    [SerializeField] float partUpWeight = 50f;
    [SerializeField] float newPartWeight = 20f;
    [SerializeField] float statWeight = 30f;

    public List<UpgradeOption> current = new List<UpgradeOption>();

    public void UpgradeStart()
    {
        var partUp = new List<UpgradeOption>();
        var newPart = new List<UpgradeOption>();
        var stat = new List<UpgradeOption>();
        BuildPools(CarManager.Inst, partUp, newPart, stat);

        var groups = new List<(List<UpgradeOption> list, float weight)>
        {
            (partUp, partUpWeight),
            (newPart, newPartWeight),
            (stat, statWeight),
        };

        // 버튼 수만큼 가중치로 뽑기
        var pool = new List<UpgradeOption>();
        for (int i = 0; i < btns.Length; i++)
        {
            var opt = PickWeighted(groups);
            if (opt == null) break;
            pool.Add(opt);
        }

        current.Clear();
        for (int i = 0; i < btns.Length; i++)
        {
            bool has = i < pool.Count;
            btns[i].gameObject.SetActive(has);
            if (!has) continue;

            current.Add(pool[i]);

            var texts = btns[i].GetComponentsInChildren<TMP_Text>();
            texts[0].text = pool[i].title;
            if (texts.Length > 1) texts[1].text = pool[i].desc;

            int index = i;
            btns[i].onClick.RemoveAllListeners();
            btns[i].onClick.AddListener(() => UpgradeComplete(index));
        }

        if (current.Count == 0) return;

        Time.timeScale = 0f;
        upgradeUI.Open();
    }
    public void UpgradeComplete(int select)
    {
        if (select < 0 || select >= current.Count) return;

        current[select].apply();
        current.Clear();

        upgradeUI.Close();
        Time.timeScale = 1f;
    }

    void BuildPools(CarManager car,
    List<UpgradeOption> partUp, List<UpgradeOption> newPart, List<UpgradeOption> stat)
    {
        // 1. 가진 부품 강화
        foreach (var part in car.Parts)
        {
            if (part == null || part.grade >= part.MaxGrade) continue;

            var p = part;
            UpgradeText text = p.GetExplain();
            partUp.Add(new UpgradeOption
            {
                title = text.title,
                desc = text.line,
                apply = () => p.Upgrade()
            });
        }

        // 2. 새 부품 (빈 슬롯이 있을 때만)
        if (car.GetEmptySlot() >= 0)
        {
            foreach (var item in ItemData.GetItemAll)
            {
                if (!item.HasTag("part") || car.HasPart(item.id)) continue;

                string id = item.id;
                newPart.Add(new UpgradeOption
                {
                    title = item.itemName,
                    desc = "새 부품",
                    apply = () => UIManager.MainInventory.AddItem(id)
                });
            }
        }

        // 3. 스테이터스
        stat.Add(StatOption(car, StatType.Atk, "공격력", atkUp));
        stat.Add(StatOption(car, StatType.Speed, "이동 속도", speedUp));
        stat.Add(StatOption(car, StatType.CritChance, "치명타 확률", critChanceUp));
        stat.Add(StatOption(car, StatType.CritAtk, "치명타 피해", critAtkUp));
        stat.Add(StatOption(car, StatType.Resistance, "저항", resistanceUp));
    }

    UpgradeOption StatOption(CarManager car, StatType type, string name, float amount)
    {
        return new UpgradeOption
        {
            title = $"{name} 증가",
            desc = $"+{amount}",
            apply = () =>
            {
                var s = car.Stat;
                switch (type)
                {
                    case StatType.Atk: s.atk += amount; break;
                    case StatType.Speed: s.speed += amount; break;
                    case StatType.CritChance: s.critChance += amount; break;
                    case StatType.CritAtk: s.critAtk += amount; break;
                    case StatType.Resistance: s.resistance += amount; break;
                }
            }
        };
    }

    static UpgradeOption PickWeighted(List<(List<UpgradeOption> list, float weight)> groups)
    {
        float total = 0f;
        foreach (var g in groups)
            if (g.list.Count > 0) total += g.weight;

        if (total <= 0f) return null;

        float r = UnityEngine.Random.value * total;
        foreach (var g in groups)
        {
            if (g.list.Count == 0) continue;
            r -= g.weight;
            if (r > 0f) continue;

            int idx = UnityEngine.Random.Range(0, g.list.Count);
            var opt = g.list[idx];
            g.list.RemoveAt(idx);
            return opt;
        }
        return null;
    }
}
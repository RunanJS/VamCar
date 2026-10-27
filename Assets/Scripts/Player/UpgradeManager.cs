using System;
using System.Collections.Generic;
using System.Linq;
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

    public List<UpgradeOption> current = new List<UpgradeOption>();

    public void UpgradeStart()
    {
        var pool = BuildPool(CarManager.Inst);
        Shuffle(pool);

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

    List<UpgradeOption> BuildPool(CarManager car)
    {
        var pool = new List<UpgradeOption>();

        // 1. 가진 부품 강화 (적용 내용은 Upgrade()가 반환)
        foreach (var part in car.Parts)
        {
            if (part == null || part.grade >= part.MaxGrade) continue;

            var p = part;
            UpgradeText text = p.GetExplain();
            pool.Add(new UpgradeOption
            {
                title = text.title,
                desc = text.line,
                apply = () => p.Upgrade()
            });
        }

        // 2. 새 부품 (빈 슬롯이 있을 때만)
        int slot = car.GetEmptySlot();
        if (slot >= 0)
        {
            foreach (var item in ItemData.GetItemAll)
            {
                if (!item.HasTag("part") || car.HasPart(item.id)) continue;

                string id = item.id;
                string name = item.itemName;
                pool.Add(new UpgradeOption
                {
                    title = item.itemName,
                    desc = "새 부품",
                    apply = () => car.SetPart(slot, id)
                });
            }
        }

        // 3. 스테이터스
        pool.Add(StatOption(car, StatType.Atk, "공격력", atkUp));
        pool.Add(StatOption(car, StatType.Speed, "이동 속도", speedUp));
        pool.Add(StatOption(car, StatType.CritChance, "치명타 확률", critChanceUp));
        pool.Add(StatOption(car, StatType.CritAtk, "치명타 피해", critAtkUp));
        pool.Add(StatOption(car, StatType.Resistance, "저항", resistanceUp));

        return pool;
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

    static void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
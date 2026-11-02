using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public static CarManager Inst;

    [Header("차")]
    public CarController carController;
    public CarStat carStat;

    [Header("파츠가 붙는 위치")]
    [SerializeField] Transform[] slotPoint = new Transform[6];

    [Header("부품 슬롯")]
    [SerializeField] PartBase[] partSlot = new PartBase[6];
    List<ISpeed> enginePowers = new List<ISpeed>();
    public IReadOnlyList<PartBase> Parts => partSlot;

    public int GetEmptySlot()
    {
        int res = 0;
        foreach (var item in partSlot)
        {
            if (item == null) res++;
        }
        return res;
    }

    public bool HasPart(string id)
    {
        foreach (var item in partSlot)
        {
            if (item == null) continue;
            if (item.id == id) return true;
        }
        return false;
    }

    [Header("레벨 관련")]
    public float exp;
    public int lv;
    //[SerializeField] AnimationCurve expCurve;
    public float RequiredExp => Mathf.RoundToInt(5 * Mathf.Pow(lv, 1.1f));

    [Header("업그레이드 관련")]
    [SerializeField] UpgradeManager upgradeManager;


    // 스탯
    public Status Stat => carStat.stat;

    public float EnginePower
    {
        get
        {
            float powerAdd = 0;
            foreach (var item in enginePowers)
            {
                powerAdd += item.Power;
            }
            return 1 + powerAdd / 10;
        }
    }

    public void SetPart(int index, PartBase part)
    {
        // 둘 다 null이면 (헛클릭) 스킵
        if (partSlot[index] == null && part == null) return;

        // 장착
        partSlot[index] = Instantiate(part, slotPoint[index]
            ).GetComponent<PartBase>();

        // 창고 등록
        if (partSlot[index] is StorageBase addStorage)
            UIManager.SetStorageSize(addStorage.size);

        // 인벤 쓰는 애면 등록
        if (partSlot[index] is IConnecter connecter)
            UIManager.Register(connecter.ConnectedInventory);

        // 엔진출력 등록
        if (partSlot[index] is ISpeed power)
            enginePowers.Add(power);

    }

    public void SeparatePart(int index)
    {
        // 슬롯 비어있으면 스킵
        if (partSlot[index] == null) return;

        // 창고 등록해제
        if (partSlot[index] is StorageBase removeStorage)
            UIManager.SetStorageSize(-removeStorage.size);

        // 인벤 쓰는 애면 등록 해제
        if (partSlot[index] is IConnecter connectRemoved)
            UIManager.UnRegister(connectRemoved.ConnectedInventory);

        // 엔진출력 등록해제
        if (partSlot[index] is ISpeed power)
            enginePowers.Remove(power);

        Destroy(partSlot[index].gameObject);
        partSlot[index] = null;
    }

    void Awake()
    {
        if (Inst == null) Inst = this;
        else Destroy(this);

    }

    void Start()
    {
        for (int i = 0; i < partSlot.Length; i++)
        {
            if (partSlot[i] != null)
            {
                SetPart(i, partSlot[i]);
            }
        }
    }

    private void Update()
    {
        foreach (var part in partSlot)
        {
            if (part == null) continue;
            if (part.IsActivated) part.timer += Time.deltaTime;
            if (part.timer > part.GetTime)
            {
                part.timer = 0;
                part.SkillEffect();
            }
        }
    }

    private void FixedUpdate()
    {
        if (carStat.IsDead) return;
        carController.Move(Stat.speed * EnginePower);
    }

    public event Action<float, float, int> OnExpChanged;

    public void AddExp(float _exp)
    {
        exp += _exp;
        if (exp >= RequiredExp) LevelUp();
        OnExpChanged?.Invoke(exp, RequiredExp, lv);
    }

    private void LevelUp()
    {
        AddExp(-RequiredExp);
        lv++;
        upgradeManager.UpgradeStart();
    }

    public void OnTriggerStay(Collider other)
    {
        // 아이템 먹기
        if (other.TryGetComponent<ItemObject>(out ItemObject item)
            && item.getable)
        {
            if (UIManager.MainInventory.CanGetItem(item.GetItem))
                UIManager.MainInventory.AddItem(item.UseItem());
        }
    }
}
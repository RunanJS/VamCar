using System;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : InventoryUI
{
    public static CarManager Inst;

    [Header("차")]
    public CarController carController;
    public CarStat carStat;

    [Header("파츠가 붙는 위치")]
    [SerializeField] Transform[] slotPoint = new Transform[6];

    [Header("부품 슬롯")]
    [SerializeField] PartBase[] slot = new PartBase[6];
    List<ISpeed> enginePowers = new List<ISpeed>();
    public IReadOnlyList<PartBase> Parts { get; }
    public int GetEmptySlot()
    {
        int res = 0;
        foreach (var item in slot)
        {
            if (item == null) res++;
        }
        return res;
    }
    public bool HasPart(string id)
    {
        foreach (var item in slot)
        {
            if (item.name == id) return true;
        }
        return false;
    }

    [Header("레벨 관련")]
    public float exp;
    public int lv;
    //[SerializeField] AnimationCurve expCurve;
    public float RequiredExp => Mathf.RoundToInt(5 * Mathf.Pow(lv, 1.3f));

    [Header("업그레이드 관련")]
    UpgradeManager upgradeManager;


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

    public void SetPart(int index, string partId)
    {
        // 둘 다 null이면 (헛클릭) 스킵
        if (slot[index] == null && partId == "") return;

        // 제거
        if (slot[index] != null && partId == "")
        {
            // 창고 등록해제
            if (slot[index] is StorageBase removeStorage)
                UIManager.SetStorageSize(-removeStorage.size);

            // 인벤 쓰는 애면 등록 해제
            if (slot[index] is IConnecter connectRemoved)
                UIManager.UnRegister(connectRemoved.ConnectedInventory);

            // 엔진출력 등록해제
            if (slot[index] is ISpeed power)
                enginePowers.Remove(power);

            Destroy(slot[index].gameObject);
            slot[index] = null;
            //Debug.Log("제거");
        }
        else
        {
            // 장착
            slot[index] = Instantiate(ItemData.GetPrefab(partId), slotPoint[index]
                ).GetComponent<PartBase>();
            // 이름 설정
            slot[index].name = partId;

            // 창고 등록
            if (slot[index] is StorageBase addStorage)
                UIManager.SetStorageSize(addStorage.size);

            // 인벤 쓰는 애면 등록
            if (slot[index] is IConnecter connecter)
                UIManager.Register(connecter.ConnectedInventory);

            // 엔진출력 등록
            if (slot[index] is ISpeed power)
                enginePowers.Add(power);
        }
    }


    public override void OnSlotClickPlus(int index)
    {
        string res = inventory[index].IsEmpty ? "" : inventory[index].id;
        if (inventory[index].IsEmpty || inventory[index].item.HasTag("part"))
        {
            SetPart(index, res);
            Refresh();
        }
        else
        {
            Debug.Log("파츠아닌거끼워짐 반환");
            PlayerHand.Hand = inventory.PickUp(index, PlayerHand.Hand);
            Refresh();
        }
    }

    void Awake()
    {
        if (Inst == null) Inst = this;
        else Destroy(this);

    }

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i] != null)
            {
                inventory.SetSlot(i, ItemData.GetItem(slot[i].name), 1);
                Refresh();
                OnSlotClickPlus(i);
            }
        }
    }

    private void Update()
    {
        foreach (var part in slot)
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

    public void AddExp(float _exp)
    {
        exp += _exp;
        if (exp > RequiredExp) LevelUp();
    }

    private void LevelUp()
    {
        exp -= RequiredExp;
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
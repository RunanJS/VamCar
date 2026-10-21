using System.Collections.Generic;
using UnityEngine;

public class CarManager : InventoryUI
{
    public static CarManager Inst;

    [Header("파츠가 붙는 위치")]
    [SerializeField] Transform[] slotPoint = new Transform[6];

    // 상하전후좌우
    PartBase[] slot = new PartBase[6];
    List<ISpeed> enginePowers = new List<ISpeed>();

    [Header("레벨 관련")]
    public float exp;
    public int lv;
    //[SerializeField] AnimationCurve expCurve;
    public float RequiredExp
    {
        get { return Mathf.RoundToInt(5 * Mathf.Pow(lv, 1.3f)); }
    }

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
                UIManager.SetStorage(-removeStorage.size);

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

            // 창고 등록
            if (slot[index] is StorageBase addStorage)
                UIManager.SetStorage(addStorage.size);

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

    private void Update()
    {
        foreach (var part in slot)
        {
            if(part == null) continue;
            if (part.IsActivated) part.timer += Time.deltaTime;
            if (part.timer > part.GetTime)
            {
                part.timer = 0;
                part.SkillEffect();
            }
        }

        /*
        foreach (var item in updatedParts)
        {
            item.OnUpdate();
        }
        // 엔진에서 경험치 추출
        foreach (var item in engines)
        {
            exp += item.Generate();
            // 경험치 충족량에 다다르면
            if(exp > RequiredExp)
            {
                LevelUp();
            }
        }*/
    }

    private void LevelUp()
    {
        exp -= RequiredExp;
        lv++;
    }
}

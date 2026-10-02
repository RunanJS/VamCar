using UnityEngine;

public class CarManager : InventoryUI
{
    /*
    public PartBase<GameObject>[] top;
    public PartBase<GameObject>[] bottom;
    public PartBase<GameObject>[] left;
    public PartBase<GameObject>[] right;
    public PartBase<GameObject>[] front;
    public PartBase<GameObject>[] back;
    public PartBase<GameObject>[] inside;
    */

    [Header("파츠가 붙는 위치")]
    [SerializeField] Transform[] slotPoint = new Transform[6];

    // 상하전후좌우
    PartBase[] slot = new PartBase[6];

    public void SetPart(int index, string partId)
    {
        // 둘 다 null이면 (헛클릭) 스킵
        if (slot[index] == null && partId == "") return;

        // 제거
        if(slot[index] != null && partId == "")
        {
            Destroy(slot[index].gameObject);
            slot[index] = null;
            Debug.Log("제거");
            return;
        }

        // 장착
        slot[index] = Instantiate(ItemData.GetPrefab(partId)
            ).GetComponent<PartBase>();
        slot[index].gameObject.transform.position = slotPoint[index].position;
        slot[index].gameObject.transform.rotation = slotPoint[index].rotation;
        Debug.Log(index + "슬롯 장착");
    }

    public float EnginePower
    {
        get { return 1; }
    }

    public override void OnSlotClickPlus(int index)
    {
        string res = inventory[index].IsEmpty ? "" : inventory[index].id;
        SetPart(index, res);
    }
}

using System.Collections.Generic;
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
    List<IUpdated> updatedParts = new List<IUpdated>();

    public void SetPart(int index, string partId)
    {
        // 둘 다 null이면 (헛클릭) 스킵
        if (slot[index] == null && partId == "") return;

        // 제거
        if(slot[index] != null && partId == "")
        {
            // updated면 제거
            if (slot[index] is IUpdated removed)
            {
                updatedParts.Remove(removed);
            }

            Destroy(slot[index].gameObject);
            slot[index] = null;
            Debug.Log("제거");
        }
        else
        {
            // 장착
            slot[index] = Instantiate(ItemData.GetPrefab(partId), slotPoint[index]
                ).GetComponent<PartBase>();
        }

        // 업데이트 필요하면 등록
        if (slot[index] is IUpdated updated)
        {
            updatedParts.Add(updated);
        }
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

    private void Update()
    {
        foreach (var item in updatedParts)
        {
            item.OnUpdate();
        }
    }
}

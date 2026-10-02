using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] protected Inventory inventory;
    [SerializeField] private List<ItemSlotUI> slots = new List<ItemSlotUI>();

    [Header("인스펙터")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] protected GameObject layout;
    public int size = 32;

    private void Start()
    {
        // 미리 슬롯 설정 안해놓으면
        if (slots == null)
        {
            // 사이즈 기준으로 슬롯 생성
            inventory = new Inventory(size);

            slots = new List<ItemSlotUI>(size);
            // 인벤칸 소환
            for (int i = 0; i < inventory.Length; i++)
            {
                slots[i] = Instantiate(slotPrefab, layout.transform
                    ).GetComponent<ItemSlotUI>();
            }
        }
        else
        {
            // 아니면 슬롯 기준으로 inventory 사이즈 조정
            size = slots.Count;
            inventory = new Inventory(size);
            inventory.AddItem("saw_bumper");
        }

        for (int i = 0; i < slots.Count; i++)
        {
            int index = i;

            slots[i].SetClickEvent(() =>
            {
                OnSlotClick(index);
            });
        }
        Refresh();
    }

    // 새로고침
    public void Refresh()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].SetSlot(inventory[i]);
        }
    }

    public void OnSlotClick(int index)
    {
        PlayerHand.Hand = inventory.PickUp(index, PlayerHand.Hand);
        Refresh();
        OnSlotClickPlus(index);
    }

    public virtual void OnSlotClickPlus(int index)
    {
    }

    public void SetSlotCount(int count)
    {
        // 슬롯이 부족하면 추가 생성
        while (slots.Count < count)
        {
            ItemSlotUI slot = Instantiate(slotPrefab, layout.transform
                ).GetComponent<ItemSlotUI>();
            slots.Add(slot);
        }

        // 필요한 개수만 활성화
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(i < count);
        }

        Refresh();
    }
}
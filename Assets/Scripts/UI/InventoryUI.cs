using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private ItemSlotUI[] slots;

    [Header("인스펙터")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject layout;
    public int size = 32;

    private void Start()
    {
        inventory = new Inventory(size);
        inventory.AddItem("scrap");
        inventory.AddItem("saw_bumper");
        slots = new ItemSlotUI[size];
        // 인벤칸 소환
        for (int i = 0; i < inventory.Length; i++)
        {
            slots[i] = Instantiate(slotPrefab, layout.transform
                ).GetComponent<ItemSlotUI>();
        }

        for (int i = 0; i < slots.Length; i++)
        {
            int index = i;

            slots[i].SetClickEvent(() =>
            {
                OnSlotClick(index);
            });
        }
        Debug.Log(inventory.GetSlot(0).id);
        Refresh();
    }

    // 새로고침
    public void Refresh()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSlot(inventory[i]);
        }
    }

    public void OnSlotClick(int index)
    {
        PlayerHand.Hand = inventory.PickUp(index, PlayerHand.Hand);
        Refresh();
    }
}
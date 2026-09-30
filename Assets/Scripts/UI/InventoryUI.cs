using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private ItemSlotUI[] slots;

    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
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
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSlot(inventory[i]);
        }
    }

    public void OnSlotClick(int index)
    {
        inventory.PickUp(index, ref PlayerHand.player.hand);
        Refresh();
    }
}
using UnityEngine;

public class EngineUI : InventoryUI
{
    public void SetInventory(IConnecter _inven)
    {
        Inventory inven = _inven.ConnectedInventory;
        inventory = inven;
        SetSlotCount(inven.Length);
    }
}

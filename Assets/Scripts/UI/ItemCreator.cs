using UnityEngine;
using UnityEngine.UI;

public class ItemCreator : MonoBehaviour
{
    [SerializeField] InventoryUI target;

    public void CreateItem(string id)
    {
        if (ItemData.Get<Item>(id) == ItemData.Get<Item>("scrap")) return;
        target.inventory.AddItem(id);
        target.Refresh();
    }
}

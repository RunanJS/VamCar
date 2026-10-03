using UnityEngine;
using UnityEngine.UI;

public class ItemCreator : MonoBehaviour
{
    [SerializeField] InventoryUI target;

    public void CreateItem(string id)
    {
        target.inventory.AddItem(id);
        target.Refresh();
    }
}

using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] Canvas canvas;
    public static UIManager Instance;

    List<Inventory> inventorys = new List<Inventory>();

    [Header("버튼")]
    [SerializeField] Transform btnLayout;
    [SerializeField] Button btnPrefab;
    [SerializeField] List<Button> btns = new List<Button>();

    void OnEnable() => InputManager.OnInventoryToggle += Toggle;
    void OnDisable() => InputManager.OnInventoryToggle -= Toggle;

    void Awake()
    {
        Instance = this;
    }

    void Toggle()
    {
        canvas.enabled = !canvas.enabled;
    }

    // 일단 CarManager에서 등록해줌
    public static void Register(Inventory inven)
    {
        var m = Instance;
        m.inventorys.Add(inven);

        Button btn = Instantiate(m.btnPrefab, 
            m.btnLayout).GetComponent<Button>();
        btn.onClick.AddListener(() => m.SelectInventory(inven));
        m.btns.Add(btn);
    }

    public static void UnRegister(Inventory inven)
    {
        var m = Instance;
        int index = Instance.inventorys.IndexOf(inven);
        if (index < 0) return;

        m.inventorys.RemoveAt(index);
        Destroy(m.btns[index].gameObject);
        m.btns.RemoveAt(index);

        // 보고 있던 인벤토리가 제거되면 다른 것으로 교체
        if (m.inventoryUI.inventory == inven)
        {
            if (m.inventorys.Count > 0)
                m.SelectInventory(m.inventorys[0]);
            else
                m.inventoryUI.inventory = null;
        }
    }

    public static void SetStorage(int i)
    {
        Instance.inventoryUI.SetSlotCount(Instance.inventoryUI.size + i);
    }

    public void SelectInventory(Inventory inven)
    {
        inventoryUI.SetInventory(inven);
    }

    public void SelectInventory(int index)
    {
        if (index >= inventorys.Count) return;
        SelectInventory(inventorys[index]);
    }
}

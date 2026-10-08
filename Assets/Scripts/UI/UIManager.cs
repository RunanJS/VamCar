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
    [SerializeField] List<Button> btns = new List<Button>();

    void OnEnable() => InputManager.OnInventoryToggle += Toggle;
    void OnDisable() => InputManager.OnInventoryToggle -= Toggle;

    void Toggle()
    {
        canvas.enabled = !canvas.enabled;
    }

    // 일단 CarManager에서 등록해줌
    public static void Register(Inventory inven)
    {
        Instance.inventorys.Add(inven);
    }

    public static void UnRegister(Inventory inven)
    {
        int index = Instance.inventorys.IndexOf(inven);
        Instance.inventorys.RemoveAt(index);
        Instance.btns.RemoveAt(index);
    }
}

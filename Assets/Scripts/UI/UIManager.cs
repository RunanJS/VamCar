using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] PopupUI invenPopUi;

    List<Inventory> inventorys = new List<Inventory>();

    [Header("exp¹Ù")]
    [SerializeField] Image expBar;
    [SerializeField] TextMeshProUGUI levelText;

    [Header("¹öÆ°")]
    [SerializeField] Transform btnLayout;
    [SerializeField] Button btnPrefab;
    [SerializeField] List<Button> btns = new List<Button>();

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var car = CarManager.Inst;
        car.OnExpChanged += RefreshExp;
        RefreshExp(car.exp, car.RequiredExp, car.lv);
    }

    private void OnDestroy()
    {
        if (CarManager.Inst != null)
            CarManager.Inst.OnExpChanged -= RefreshExp;
    }

    void RefreshExp(float exp, float need, int level)
    {
        expBar.fillAmount = exp / need;
        levelText.text = $"Lv.{level}";
    }

    void Toggle()
    {
        invenPopUi.Toggle();
    }

    public void Close()
    {
        invenPopUi.Close();
    }
}

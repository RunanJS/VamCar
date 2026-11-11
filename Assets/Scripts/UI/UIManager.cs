using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    //[SerializeField] InventoryUI inventoryUI;
    [SerializeField] PopupUI invenPopUi;

    [Header("exp바")]
    [SerializeField] Image expBar;
    [SerializeField] TextMeshProUGUI levelText;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InputManager.OnMenuToggle += Toggle;

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
        float ratio = need > 0f ? Mathf.Clamp01(exp / need) : 0f;

        RectTransform fill = expBar.rectTransform;
        float parentWidth = ((RectTransform)fill.parent).rect.width;

        Vector2 offset = fill.offsetMax;
        offset.x = -parentWidth * (1f - ratio);   // 비율이 줄수록 오른쪽 끝이 왼쪽으로
        fill.offsetMax = offset;

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

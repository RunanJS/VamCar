using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private Button button;
    [SerializeField] private Color nullColor = new Color(1f, 1f, 1f, 0.2f);

    private ItemSlot slot;

    public void SetSlot(ItemSlot slot)
    {
        this.slot = slot;

        if (slot == null || slot.IsEmpty)
        {
            icon.sprite = null;
            icon.color = nullColor;
            amountText.text = "";
            return;
        }

        icon.sprite = ItemData.GetSprite(slot.item.id);

        Color color = icon.color;
        color.a = 1f;
        icon.color = color;

        amountText.text = slot.amount > 1
            ? slot.amount.ToString()
            : "";
    }

    public void SetClickEvent(Action action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => action());
    }
}
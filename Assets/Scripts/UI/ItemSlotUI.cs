using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private Button button;

    private ItemSlot slot;

    public void SetSlot(ItemSlot slot)
    {
        this.slot = slot;

        if (slot == null || slot.IsEmpty)
        {
            icon.enabled = false;
            amountText.text = "";
            return;
        }

        icon.enabled = true;
        icon.sprite = ItemData.inst.GetSprite(slot.item.id);

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
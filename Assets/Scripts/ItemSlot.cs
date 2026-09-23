using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot
{
    public Item item;
    public int amount; 
    [SerializeField] Image m_sprite;
    [SerializeField] TextMeshProUGUI m_amountText;

    public string Text
    {
        get { return m_amountText.text; }
        set { m_amountText.text = value; }
    }
    public Sprite Sprite
    {
        get { return m_sprite.sprite; }
        set { m_sprite.sprite = value; }
    }
    public string id { get { return item.id; } }

    public ItemSlot(string _itemId, int _amount)
    {
        item = ItemData.inst.GetItem(_itemId);
        amount = _amount;
    }

    public ItemSlot(Item _item = null, int _amount = 1)
    {
        item = _item;

        // 널이면 갯수는 0
        amount = _item == null ? 0 : _amount;
    }

    // 더하고 남는거 반환
    public int Add(int _amount)
    {
        int remain = item.maxStack - (amount + _amount);
        if (remain > 0) amount = item.maxStack;
        return remain;
    }

    public void Exchange(ItemSlot slot)
    {
        ItemSlot ex = this;
        item = slot.item;
        amount = slot.amount;

        slot.item = ex.item;
        slot.amount = ex.amount;

        //Refresh();
        //slot.Refresh();
    }

    public bool IsFull
    {
        get { return amount >= item.maxStack; }
    }

    public void Refresh()
    {
        if (item == null)
        {
            amount = 0;
        }

        Sprite = ItemData.inst.GetSprite(id);
    }
}
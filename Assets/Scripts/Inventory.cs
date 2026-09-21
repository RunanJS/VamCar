using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemSlot> inventory = new List<ItemSlot>();
    int length;
    public int Length { get { return length; } }

    public Inventory(int slotCount, params ItemSlot[] items)
    {
        inventory = new List<ItemSlot>();
        length = slotCount;

        for (int i = 0; i < slotCount; i++)
        {
            inventory.Add(new ItemSlot());
        }

        if(items.Length > 0)
        {
            foreach (var _item in items)
            {
                Add(_item);
            }
        }
    }

    public bool Add(ItemSlot _item)
    {
        return Add(_item.item.id, _item.amount);
    }

    public bool Add(string id, int _amount = 1)
    {
        // 아이템 수가 0개면 그냥 패스
        if (_amount <= 0) return false;

        bool res = false;

        foreach (var _item in inventory)
        {
            // 여유공간이 있는 같은 아이템이 있을 경우
            if (_item.id.Equals(id) && !_item.IsFull)
            {
                // 꽉 차면 새 슬롯에 한번 더 Add 시전하기
                Add(id, _item.Add(_amount));
                return true;
            }
        }

        // 새 슬롯에 저장

        // 슬롯 없으면 그냥 false

        return res;
    }
}

public class ItemSlot
{
    public Item item;
    public int amount;
    public string id { get { return item.id; } }

    public ItemSlot(string _itemId, int _amount)
    {
        //item = ItemData.GetItem(_itemId);
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

    public bool IsFull
    {
        get { return amount >= item.maxStack; }
    }
}

public class Item
{
    public string itemName;
    public string id;
    public int maxStack = 64;
    public List<string> nbt = new List<string>();

    public Item(string itemName, string id, int maxStack = 64, params string[] nbts)
    {
        this.itemName = itemName;
        this.id = id;
        this.maxStack = maxStack;
        if (nbts.Length > 0 && nbts != null)
        {
            foreach (var item in nbts)
            {
                nbt.Add(item);
            }
        }
    }

    public bool Is(params string[] inputNbts)
    {
        bool res = false;
        int currectCount = 0;

        foreach (var inputNbt in inputNbts)
        {
            foreach (var myNbt in nbt)
            {
                if (inputNbt.Equals(myNbt))
                {
                    currectCount++;
                    break;
                }
            }
        }

        // 일치하는 nbt 수 == 일치해야 하는 nbt 수일 때 참
        res = currectCount == inputNbts.Length ? true : false;

        return res;
    }
}
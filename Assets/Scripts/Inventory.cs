using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Inventory
{
    public ItemSlot[] inventory;
    public int Length { get { return inventory.Length; } }

    public ItemSlot this[int index]
    {
        get { return inventory[index]; }
        set { inventory[index] = value; }
    }

    public Inventory(int slotCount, params ItemSlot[] items)
    {
        inventory = new ItemSlot[slotCount];
        
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
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = new ItemSlot(id, _amount);
                return true;
            }
        }

        // 슬롯 없으면 그냥 false
        return false;
    }
}
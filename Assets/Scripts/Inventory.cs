using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemSlot[] slots;
    public int Length { get { return slots.Length; } }

    public ItemSlot this[int index]
    {
        get { return slots[index]; }
        set { slots[index] = value; }
    }

    public Inventory(int slotCount, params ItemSlot[] items)
    {
        slots = new ItemSlot[slotCount];
        
        if(items.Length > 0)
        {
            foreach (var _item in items)
            {
                AddItem(_item);
            }
        }
    }

    void Awake()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
                slots[i] = new ItemSlot();
        }
    }

    public ItemSlot GetSlot(int index)
    {
        if (index < 0 || index >= slots.Length)
            return null;

        return slots[index];
    }

    public void SetSlot(int index, Item item, int amount)
    {
        if (index < 0 || index >= slots.Length)
            return;

        slots[index].item = item;
        slots[index].amount = amount;
    }

    // 아이템이 존재하는지 확인
    public bool HasItem(string id)
    {
        foreach (ItemSlot slot in slots)
        {
            if (!slot.IsEmpty && slot.item.id == id)
                return true;
        }

        return false;
    }

    // 아이템 수량이 충분한지 확인
    public bool HasItem(string id, int amount)
    {
        int count = 0;

        foreach (ItemSlot slot in slots)
        {
            if (!slot.IsEmpty && slot.item.id == id)
            {
                count += slot.amount;

                if (count >= amount)
                    return true;
            }
        }

        return false;
    }

    // 아이템 개수 세기
    public int GetItemCount(string id)
    {
        int count = 0;

        foreach (ItemSlot slot in slots)
        {
            if (!slot.IsEmpty && slot.item.id == id)
                count += slot.amount;
        }

        return count;
    }

    public int AddItem(ItemSlot _item)
    {
        return AddItem(_item.item.id, _item.amount);
    }

    public int AddItem(string id, int _amount = 1)
    {
        // 아이템 수가 0개면 그냥 패스
        if (_amount <= 0) return 0;

        ItemSlot slot = null;

        foreach (var _item in slots)
        {
            if (_item == null || _item.IsEmpty) continue;
            // 여유공간이 있는 같은 아이템이 있을 경우
            if (_item.item.id.Equals(id) && !_item.IsFull)
            {
                slot = _item;
                break;
            }
        }

        // 없으면 빈 슬롯 찾기
        if (slot == null)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == null)
                {
                    slots[i] = new ItemSlot(id, 0);
                    slot = slots[i];
                    break;
                }
            }
        }

        // 슬롯 없으면 그냥 받은만큼 돌려줌
        if (slot == null) return _amount;

        // 합친게
        int add = slot.amount + _amount;
        // 최대스택보다 많으면
        if (add > slot.item.maxStack)
        {
            // 최대스택으로 만들고
            slot.amount = slot.item.maxStack;
            // 남은걸 다시 AddItem 돌린다(새로운 슬롯으로)
            return AddItem(id, add - slot.item.maxStack);
        }
        else
        {
            // 꽉 안차면 그냥 끝
            slot.amount = add;
            return 0;
        }
    }

    public bool RemoveItem(string id, int amount)
    {
        if (amount <= 0) return false;
        // 필요한만큼 없으면 그냥 false
        if (!HasItem(id, amount)) return false;

        foreach (ItemSlot slot in slots)
        {
            if (slot == null || slot.IsEmpty) continue;

            if (slot.item.id.Equals(id))
            {
                if (slot.amount > amount)
                {
                    // 이 슬롯에서 처리가 끝나는 경우
                    slot.amount -= amount;
                    return true;
                }

                // 한 묶음 없애기
                amount -= slot.amount;
                slot.Clear();

                // 한 묶음 없애기로 끝나면 반환
                if (amount <= 0) return true;
            }
        }

        return true;
    }

    // 두 슬롯 교환
    public void Swap(int indexA, int indexB)
    {
        if (indexA < 0 || indexA >= slots.Length) return;

        if (indexB < 0 || indexB >= slots.Length) return;

        ItemSlot temp = slots[indexA];

        slots[indexA] = slots[indexB];
        slots[indexB] = temp;
    }

    // 교체
    public void PickUp(int index, ref ItemSlot slot)
    {
        if (index < 0 || index >= slots.Length) return;
        if(slot == null && slots[index] == null) return;
        if (slot.IsEmpty && slots[index].IsEmpty) return;

        // 빈손일때
        if (slot == null || slot.IsEmpty)
            slot = new ItemSlot();
        if (slots == null || slots[index].IsEmpty) 
            slots[index] = new ItemSlot();

        // 아이템이 같으면
        if (slot.item.id.Equals(slots[index].item.id))
        {
            // 합친다
            int add = slots[index].amount + slot.amount;

            if(add > slots[index].item.maxStack)
            {
                slots[index].amount = slots[index].item.maxStack;
                slot.amount = add - slots[index].item.maxStack;
            }
            else
            {
                slots[index].amount = add;
                slot.Clear();
            }
        } 
        else // 아이템이 다르면
        {
            // 바꾼다
            ItemSlot ex = new ItemSlot(slot);
            slot = new ItemSlot(slots[index]);
            slots[index] = ex;
        }
    }

    public void Move(int from, int to)
    {
        if (from < 0 || from >= slots.Length)
            return;

        if (to < 0 || to >= slots.Length)
            return;

        if (from == to)
            return;

        ItemSlot source = slots[from];
        ItemSlot target = slots[to];

        if (source.IsEmpty)
            return;


        // 빈 슬롯
        if (target.IsEmpty)
        {
            slots[to] = source;
            slots[from] = new ItemSlot();
            return;
        }


        // 같은 아이템 → 합치기
        if (target.item.id == source.item.id)
        {
            int space = target.item.maxStack - target.amount;

            int moveAmount = Mathf.Min(space, source.amount);

            target.amount += moveAmount;
            source.amount -= moveAmount;

            if (source.amount <= 0)
                source.Clear();

            return;
        }


        // 다른 아이템 → 교환
        Swap(from, to);
    }

    // 슬롯 하나 비우기
    public void ClearSlot(int index)
    {
        if (index < 0 || index >= slots.Length)
            return;

        slots[index].Clear();
    }

    // 비어있는지 확인
    public bool IsEmpty()
    {
        foreach (ItemSlot slot in slots)
        {
            if (!slot.IsEmpty)
                return false;
        }

        return true;
    }

    // 빈 슬롯 찾기
    public int FindEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty)
                return i;
        }

        return -1;
    }

    // 특정 아이템이 들어있는 슬롯 찾기
    public int FindItemSlot(string id)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].IsEmpty && slots[i].item.id == id)
                return i;
        }

        return -1;
    }
}
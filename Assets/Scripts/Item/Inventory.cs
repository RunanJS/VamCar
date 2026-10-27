using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : IEnumerable<ItemSlot>
{
    [SerializeField] private List<ItemSlot> slots = new List<ItemSlot>();
    public event Action OnChanged;
    void NotifyChanged() => OnChanged?.Invoke();

    public int Length { get { return slots.Count; } }

    public ItemSlot this[int index]
    {
        get { return slots[index]; }
        set { slots[index] = value; }
    }

    public Inventory(int slotCount, params ItemSlot[] items)
    {
        slots = new List<ItemSlot>();
        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(new ItemSlot());
        }
        
        if(items.Length > 0)
        {
            foreach (var _item in items)
            {
                AddItem(_item);
            }
        }
    }

    public ItemSlot GetSlot(int index)
    {
        if (index < 0 || index >= Length)
            return null;

        return slots[index];
    }

    public void SetSlot(int index, Item item, int amount)
    {
        if (index < 0 || index >= Length)
            return;

        slots[index].item = item;
        slots[index].amount = amount;
        NotifyChanged();
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

    public bool HasItem(int amount)
    {
        int count = 0;

        foreach (ItemSlot slot in slots)
        {
            if (slot.IsEmpty) continue;

            count += slot.amount;

            if (count >= amount)
                return true;
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

    // 조금이라도 받을 공간이 있는지
    
    public bool CanGetItem(ItemSlot slot)
    {
        return CanGetItem(slot.id, slot.amount);
    }
    
    public bool CanGetItem(string id, int _amount)
    {
        // 아이템 수가 0개면 그냥 패스
        if (_amount <= 0) return true;

        foreach (var _item in slots)
        {
            // 비어있거나 여유공간이 있는 같은 아이템이 있을 경우
            if (_item.IsEmpty || (_item.item.id.Equals(id) && !_item.IsFull))
            {
                return true;
            }
        }
        return false;
    }

    // 비지정 슬롯에 아이템 획득
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
            if (_item.IsEmpty) continue;
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
            for (int i = 0; i < Length; i++)
            {
                if (slots[i] == null || slots[i].IsEmpty)
                {
                    slots[i] = new ItemSlot(id, 0);
                    slot = slots[i];
                    break;
                }
            }
        }

        // 슬롯 없으면 그냥 받은만큼 돌려줌
        if (slot == null)
        {
            ItemData.DropItem(CarManager.Inst.transform.position,
                id, _amount);
            return _amount;
        }

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
            //Debug.Log(id);
            NotifyChanged();
            return 0;
        }
    }

    // 맨 위부터 특정 아이템 삭제
    public bool RemoveItem(ItemSlot _item)
    {
        return RemoveItem(_item.id, _item.amount);
    }

    public bool RemoveItem(ItemSlot _item, int amount)
    {
        return RemoveItem(_item.id, amount);
    }

    public bool RemoveItem(string id, int amount)
    {
        if (amount <= 0) return false;
        // 필요한만큼 없으면 그냥 false
        if (!HasItem(id, amount)) return false;

        foreach (ItemSlot slot in slots)
        {
            if (slot.IsEmpty) continue;

            if (slot.item.id.Equals(id))
            {
                if (slot.amount > amount)
                {
                    // 이 슬롯에서 처리가 끝나는 경우
                    slot.amount -= amount;
                    NotifyChanged();
                    return true;
                }

                // 한 묶음 없애기
                amount -= slot.amount;
                slot.Clear();
                NotifyChanged();
                // 한 묶음 없애기로 끝나면 반환
                if (amount <= 0) return true;
            }
        }

        return true;
    }

    // 최상단 템있는 슬롯 아무거나 하나
    public bool RemoveAnyItem(int amount = 1)
    {
        if (amount <= 0) return false;
        if (!HasItem(amount)) return false;

        foreach (var slot in slots)
        {
            if (slot.IsEmpty) continue;

            if(slot.amount < amount)
            {
                amount -= slot.amount;
                slot.Clear();
            }
            else
            {
                slot.amount -= amount;
                if (slot.amount <= 0) slot.Clear();
                NotifyChanged();
                return true;
            }
        }
        return false;
    }

    // Tag가 맞는 아이템의 총 수량
    public int CountTagItem(string tag)
    {
        int count = 0;
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.item.HasTag(tag))
                count += slot.amount;
        }
        return count;
    }

    public bool RemoveTagItem(string tag, int amount)
    {
        if (amount <= 0) return false;
        // 부족하면 아무것도 지우지 않음
        if (CountTagItem(tag) < amount) return false;

        foreach (var slot in slots)
        {
            if (slot.IsEmpty || !slot.item.HasTag(tag)) continue;

            int take = Mathf.Min(slot.amount, amount);
            slot.amount -= take;
            amount -= take;

            if (slot.amount <= 0) slot.Clear();
            if (amount <= 0)
            {
                NotifyChanged();
                return true;
            }

        }
        NotifyChanged();
        return true;
    }

    // Stat가 맞는 아이템의 총 수량
    public int CountStatItem(string stat)
    {
        int count = 0;
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.item.HasStat(stat))
                count += slot.amount;
        }
        return count;
    }

    public float RemoveStatItem(string stat,int amount = 1)
    {
        if (amount <= 0) return 0;
        // 부족하면 아무것도 지우지 않음
        if (CountStatItem(stat) < amount) return 0;

        float res = 0;
        foreach (var slot in slots)
        {
            if (slot.IsEmpty || !slot.item.HasStat(stat)) continue;

            int take = Mathf.Min(slot.amount, amount);

            res += take;

            slot.amount -= take;
            amount -= take;

            if (slot.amount <= 0) slot.Clear();
            if (amount <= 0)
            {
                NotifyChanged();
                // 받은 벨류만큼 반환
                return res;
            }

        }
        NotifyChanged();
        return res;
    }

    // 두 슬롯 교환
    public void Swap(int indexA, int indexB)
    {
        if (indexA < 0 || indexA >= Length) return;

        if (indexB < 0 || indexB >= Length) return;

        ItemSlot temp = slots[indexA];

        slots[indexA] = slots[indexB];
        slots[indexB] = temp;
        NotifyChanged();
    }

    // 교체
    public ItemSlot PickUp(int index, ItemSlot _slot)
    {
        ItemSlot slot = _slot;
        if (index < 0 || index >= Length)
            return slot;

        ItemSlot target = slots[index];

        // 둘 다 비어있으면 아무것도 안 함
        if ((slot == null || slot.IsEmpty) &&
            (target == null || target.IsEmpty))
            return slot;

        // 손이 비어있으면 그대로 가져오기
        if (slot == null || slot.IsEmpty)
        {
            slot = target;
            slots[index] = new ItemSlot();
            //Debug.Log("가져오기" + slot.id);
            NotifyChanged();
            return slot;
        }

        // 인벤토리 슬롯이 비어있으면 그대로 넣기
        if (target == null || target.IsEmpty)
        {
            slots[index] = slot;
            slot = new ItemSlot();
            //Debug.Log("넣기" + slots[index].id);
            NotifyChanged();
            return slot;
        }

        // 둘 다 아이템이 있는 상태
        if (slot.item.id == target.item.id)
        {
            // 같은 아이템이면 합치기
            int add = target.amount + slot.amount;
            int maxStack = target.item.maxStack;

            if (add <= maxStack)
            {
                target.amount = add;
                slot.Clear();
            }
            else
            {
                target.amount = maxStack;
                slot.amount = add - maxStack;
            }
            NotifyChanged();
            Debug.Log("합치기" + target.id + " " + target.amount + "개");
            return slot;
        }

        // 서로 다른 아이템이면 교체
        slots[index] = slot;
        slot = target;
        Debug.Log("바꾸기" + slot.id + ", " + slots[index]);
        NotifyChanged();
        return slot;
    }

    public void Move(int from, int to)
    {
        if (from < 0 || from >= Length)
            return;

        if (to < 0 || to >= Length)
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
        if (index < 0 || index >= Length)
            return;

        slots[index].Clear();
        NotifyChanged();
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
        for (int i = 0; i < Length; i++)
        {
            if (slots[i].IsEmpty)
                return i;
        }

        return -1;
    }

    // 특정 아이템이 들어있는 슬롯 찾기
    public int FindItemSlot(string id)
    {
        for (int i = 0; i < Length; i++)
        {
            if (!slots[i].IsEmpty && slots[i].item.id == id)
                return i;
        }

        return -1;
    }

    // 인벤토리 사이즈 증가
    public void SizeUp(int value)
    {
        for (int i = 0; i < value; i++)
        {
            slots.Add(new ItemSlot());
        }
    }

    // 인벤토리 사이즈 감소
    public void SizeDown(int value)
    {
        // 줄여도 최소 인벤이 1칸은 남아야함
        if (value >= Length) return;

        for (int i = 0; i < value; i++)
        {
            ItemSlot lastSlot = new ItemSlot(slots[Length - 1]);

            // 마지막 칸 지우고
            slots.RemoveAt(Length - 1);

            // 지운 칸에 뭐 있었으면 재지급
            if (!lastSlot.IsEmpty)
            {
                AddItem(lastSlot);
            }
        }
    }

    // 사이즈 정해놓고 업다운 자동분류
    public void Resize(int value)
    {
        if (value == Length) return;

        if(value > Length)
        {
            SizeUp(value - Length);
        }
        else
        {
            SizeDown(Length - value);
        }
    }

    // 배열취급
    public IEnumerator<ItemSlot> GetEnumerator()
    {
        return ((IEnumerable<ItemSlot>)slots).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
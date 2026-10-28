using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHand : MonoBehaviour
{
    public static PlayerHand inst;

    public TextMeshProUGUI text;
    
    public ItemSlotUI itemSlot;
    private ItemSlot rememberSlot;

    public ItemSlot hand;
    public static ItemSlot Hand
    {
        get
        {
            return inst.hand;
        }
        set
        {
            inst.hand = value;
            inst.itemSlot.SetSlot(value);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(inst == null)
        {
            inst = this;
        }
        Hand = new ItemSlot();
    }

    void Update()
    {
        if (hand != null)
        {
            transform.position = Vector2.Lerp(transform.position,
                Mouse.current.position.ReadValue(), 0.7f);
        }

    }

    // 손에 있던거 돌려놓기
    public void Back()
    {
        // 비어있으면 스킵
        if (hand.IsEmpty) return;

        // 메인 인벤토리에 넣고, 못 넣은 만큼은 손에 남김
        int left = UIManager.MainInventory.AddItem(hand.item.id, hand.amount);

        if (left <= 0)
        {
            Hand = new ItemSlot();            // 전부 들어감 → 손 비우기
        }
        else
        {
            hand.amount = left;               // 일부만 들어감 → 남은 개수만 유지
            Hand = hand;                      // UI 갱신
        }
    }

    // 손에서 떨구기
    public void Drop()
    {
        if (hand.IsEmpty) return;

        Transform car = CarManager.Inst.transform;
        Vector3 pos = car.position + car.forward * 2f + Vector3.up * 0.5f;

        ItemData.DropItem(pos, hand.item.id, hand.amount);
        Hand = new ItemSlot();
    }

    public void CreateItem(string id)
    {
        Hand = new ItemSlot(ItemData.GetItem(id));
    }
}

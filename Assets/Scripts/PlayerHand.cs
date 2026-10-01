using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHand : MonoBehaviour
{
    public static PlayerHand inst;

    public TextMeshProUGUI text;
    
    public ItemSlotUI itemSlot;

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
    void Start()
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

    public void CreateItem(string id)
    {
        Hand = new ItemSlot(ItemData.GetItem(id));
    }
}

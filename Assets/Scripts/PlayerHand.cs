using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHand : MonoBehaviour
{
    public static PlayerHand player;

    public TextMeshProUGUI text;

    ItemSlot hand;
    public ItemSlot Hand
    {
        get
        {
            return hand;
        }
        set
        {
            hand = value;
            text.text = hand == null ? "" : hand.amount.ToString();
            Debug.Log("get : " + hand.id);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(player == null)
        {
            player = this;
        }
    }

    void Update()
    {
        if (hand != null)
        {
            //transform.position = Vector2.Lerp(transform.position,
            //    Mouse.current.position.ReadValue(), 0.7f);
        }

    }

    public void CreateItem(string id)
    {
        Hand = new ItemSlot(ItemData.inst.GetItem(id));
    }
}

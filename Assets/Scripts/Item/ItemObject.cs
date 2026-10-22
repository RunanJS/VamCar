using System.Data;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private string id = "scrap";
    private int amount = 1;
    [SerializeField] private float dropCool = 0.5f;
    SpriteRenderer sprite;

    public bool getable => dropCool <= 0;

    public string Id => id;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        dropCool = 0.5f;
        Refresh();
    }

    public ItemSlot GetItem()
    {
        Destroy(gameObject);
        return new ItemSlot(id, amount);
    }

    public void SetItem(string id, int amount)
    {
        this.id = id;
        this.amount = amount;
        Refresh();
    }

    public void Refresh()
    {
        sprite.sprite = ItemData.GetSprite(id);
        if(amount <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime);
        if (dropCool > 0) dropCool -= Time.deltaTime;
    }
}

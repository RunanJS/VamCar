using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public static ItemData inst;
    Dictionary<string, Item> items = new Dictionary<string, Item>();
    Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    public Item GetItem(string _id)
    {
        return items[_id];
    }

    public Sprite GetSprite(string _id)
    {
        return sprites[_id];
    }

    void Awake()
    {
        if (inst == null) inst = this;

        TextAsset json = Resources.Load<TextAsset>("Items");

        Debug.Log(json == null ? "JSON NULL" : "JSON OK");

        ItemDatas data = JsonUtility.FromJson<ItemDatas>(json.text);

        foreach (var item in data.items)
        {
            items.Add(item.id, item);

            //Sprite _sprite = Resources.Load<Sprite>("Sprites/"+item.id);
            //sprites.Add(item.id, _sprite);
            Debug.Log("Set. " + item.id);
        }
    }
}

[System.Serializable]
public class ItemDatas
{
    public Item[] items;
}
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public static ItemData inst;
    Dictionary<string, Item> items = new Dictionary<string, Item>();
    Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    public static Item GetItem(string _id)
    {
        Item res = inst.items[_id];
        return res;
    }

    public static Sprite GetSprite(string _id)
    {
        return inst.sprites[_id];
    }

    public static GameObject GetPrefab(string _id)
    {
        return inst.prefabs[_id];
    }

    void Awake()
    {
        if (inst == null) inst = this;

        TextAsset json = Resources.Load<TextAsset>("Items");

        Debug.Log(json == null ? "JSON NULL" : "JSON OK");

        ItemDatas data = JsonUtility.FromJson<ItemDatas>(json.text);

        foreach (var item in data.items)
        {
            // 아이템 저장
            items.Add(item.id, item);

            // 스프라이트 저장
            Sprite _sprite = Resources.Load<Sprite>("Sprites/"+item.id);
            sprites.Add(item.id, _sprite);

            // 만약에 부품 nbt가 붙으면
            if (item.HasNbt("part"))
            {
                // 프리팹 저장
                GameObject _prefab = Resources.Load<GameObject>("Prefabs/" + item.id);
                prefabs.Add(item.id, _prefab);
            }
        }
    }
}

[System.Serializable]
public class ItemDatas
{
    public Item[] items;
}
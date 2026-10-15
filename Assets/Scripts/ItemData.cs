using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

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
        if (json == null)
        {
            Debug.LogError("Resources/Items.json 파일을 찾을 수 없습니다.");
            return;
        }

        ItemDatas data = JsonConvert.DeserializeObject<ItemDatas>(json.text);
        if (data == null || data.items == null)
        {
            Debug.LogError("Items.json 형식이 올바르지 않습니다.");
            return;
        }

        foreach (var item in data.items)
        {
            // 중복 id 방지
            if (items.ContainsKey(item.id))
            {
                Debug.LogWarning($"중복된 아이템 id: {item.id}");
                continue;
            }

            // 아이템 저장
            items.Add(item.id, item);

            // 스프라이트 저장
            Sprite sprite = Resources.Load<Sprite>("Sprites/" + item.id);
            if (sprite == null) Debug.LogWarning($"스프라이트 없음: {item.id}");
            sprites.Add(item.id, sprite);

            // 부품 태그가 있으면 프리팹 저장
            if (item.HasTag("part"))
            {
                GameObject prefab = Resources.Load<GameObject>("Prefabs/" + item.id);
                if (prefab == null) Debug.LogWarning($"프리팹 없음: {item.id}");
                prefabs.Add(item.id, prefab);
            }
        }
    }
}

[System.Serializable]
public class ItemDatas
{
    public Item[] items;
}
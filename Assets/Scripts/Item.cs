using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public string itemName;
    public string id;
    public int maxStack = 64;
    public HashSet<string> tags = new HashSet<string>();
    public Dictionary<string, float> stats = new Dictionary<string, float>();

    public Item() { }

    public Item(string itemName, string id, int maxStack = 64,
                IEnumerable<string> tags = null,
                Dictionary<string, float> stats = null)
    {
        this.itemName = itemName;
        this.id = id;
        this.maxStack = maxStack;

        if (tags != null) this.tags = new HashSet<string>(tags);
        if (stats != null) this.stats = new Dictionary<string, float>(stats);
    }

    // 복사 생성자
    public Item(Item _item)
    {
        itemName = _item.itemName;
        id = _item.id;
        maxStack = _item.maxStack;
        tags = new HashSet<string>(_item.tags);
        stats = new Dictionary<string, float>(_item.stats);
    }

    // 태그가 있는지
    public bool HasTag(string tag) => tags.Contains(tag);

    // 모든 태그를 가지고 있는지
    public bool HasTags(params string[] inputTags) => tags.IsSupersetOf(inputTags);

    // 스탯이 있는지
    public bool HasStat(string key) => stats.ContainsKey(key);

    // 스탯 값 읽기
    public float GetStat(string key, float defaultValue = 0f)
        => stats.TryGetValue(key, out float v) ? v : defaultValue;
}
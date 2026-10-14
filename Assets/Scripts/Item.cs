using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public string itemName;
    public string id;
    public int maxStack = 64;
    public Dictionary<string, float> nbt = new Dictionary<string, float>();

    /*public Item(string itemName, string id, int maxStack = 64, params string[] nbts)
    {
        this.itemName = itemName;
        this.id = id;
        this.maxStack = maxStack;
        if (nbts.Length > 0 && nbts != null)
        {
            foreach (var item in nbts)
            {
                nbt.Add(item);
            }
        }
    }*/
    public Item(string itemName, string id, int maxStack = 64,
            params (string key, float value)[] nbts)
    {
        this.itemName = itemName;
        this.id = id;
        this.maxStack = maxStack;

        if (nbts == null) return;

        foreach (var (key, value) in nbts)
        {
            nbt[key] = value;   // 같은 키가 두 번 들어와도 오류 없이 덮어씀
        }
    }

    public Item(Item _item)
    {
        this.itemName = _item.itemName;
        this.id = _item.id;
        this.maxStack = _item.maxStack;
        this.nbt = new Dictionary<string,float>(_item.nbt);
    }

    // nbt 체크
    public bool HasNbt(params string[] inputNbts)
    {
        bool res = false;
        int currectCount = 0;

        foreach (var inputNbt in inputNbts)
        {
            foreach (var myNbt in nbt)
            {
                if (inputNbt.Equals(myNbt))
                {
                    currectCount++;
                    break;
                }
            }
        }

        // 일치하는 nbt 수 == 일치해야 하는 nbt 수일 때 참
        res = currectCount == inputNbts.Length ? true : false;

        return res;
    }
}
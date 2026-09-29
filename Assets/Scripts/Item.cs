using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public string itemName;
    public string id;
    public int maxStack = 64;
    public List<string> nbt = new List<string>();

    public Item(string itemName, string id, int maxStack = 64, params string[] nbts)
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
    }

    public Item(Item _item)
    {
        this.itemName = _item.itemName;
        this.id = _item.id;
        this.maxStack = _item.maxStack;
        this.nbt = new List<string>(_item.nbt);
    }

    /*
    // 더하고 남는거 반환
    public Item Add(int _amount)
    {
        int remain = amount + _amount - maxStack;

        if (remain <= 0)
        {
            amount += _amount;
            return null;
        }

        amount = maxStack;

        Item res = new Item(this);
        res.amount = remain;
        return res;
    }
    */

    // nbt 체크
    public bool Is(params string[] inputNbts)
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
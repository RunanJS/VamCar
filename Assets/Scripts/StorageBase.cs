using UnityEngine;

public abstract class StorageBase : PartBase<GameObject>
{

    Inventory inventory = new Inventory(1);
    public bool SkillEffect(GameObject t)
    {
        throw new System.NotImplementedException();
    }
}

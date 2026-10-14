using UnityEngine;

public class FireEngine : EngineBase, IConnecter
{
    public Inventory storage = new Inventory(1);

    public Inventory ConnectedInventory => storage;

    protected override bool GetRequired
    {
        get
        {
            // 타는템 하나 없애고 발전량 전송
            if (storage.RemoveNbtItem("burn",1))
            {
                Debug.Log("발전");
                return true;
            }
            return false;
        }
    }

    public override void SkillEffect(SkillContext context)
    {
        new SkillContext();
    }
}

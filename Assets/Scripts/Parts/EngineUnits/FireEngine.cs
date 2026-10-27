using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireEngine : EngineBase, IConnecter
{
    public Inventory storage = new Inventory(1);

    public Inventory ConnectedInventory => storage;

    protected override bool GetRequired
    {
        get
        {
            // 타는템 하나 없애고 발전량 전송
            float res = storage.RemoveStatItem("burn");
            if (res == 0) return false;
            else
            {
                generateValue = res;
                return true;
            }
        }
    }


    public override void SkillEffect()
    {
    }

    public override UpgradeText Upgrade()
    {
        throw new System.NotImplementedException();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class AnythingEngine : EngineBase, IConnecter
{
    public Inventory storage = new Inventory(1);

    public Inventory ConnectedInventory => storage;

    protected override bool GetRequired
    {
        get 
        {
            // 저장소가 비어있지 않으면
            if (storage.HasItem(1))
            {
                // 아무템 하나 없애고 발전량 전송
                Debug.Log("발전");
                storage.RemoveAnyItem(1);
                return true;
            }
            return false;
        }
    }

    public override void SkillEffect(SkillContext context)
    {
        
    }
}

using System.Collections.Generic;
using UnityEngine;

public class AnythingEngine : EngineBase, IConnecter
{
    public Inventory storage = new Inventory(5);

    public Inventory ConnectedInventory => storage;

    protected override float GetGenerate
    {
        get
        {
            // 저장소가 비어있지 않으면
            if (storage.HasItem(1))
            {
                // 아무템 하나 없애고 발전량 전송
                Debug.Log("발전");
                storage.RemoveAnyItem(1);
                return generateAmount;
            }
            else return 0;
        }
    }

    public override void SkillEffect(SkillContext context)
    {
        
    }
}

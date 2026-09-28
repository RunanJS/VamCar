using System.Collections.Generic;
using UnityEngine;

public class AnythingEngine : EngineBase
{
    public Inventory storage = new Inventory(5);

    protected override float Require
    {
        get
        {
            foreach (var item in storage.inventory)
            {
                if(item != null)
                {

                }
            }



            if (storage[0] != null)
            {
                storage[0].amount -= 1;
                return generateAmount;
            }
            else return 0;
        }
    }

    public override void SkillEffect(SkillContext context)
    {
        
    }
}

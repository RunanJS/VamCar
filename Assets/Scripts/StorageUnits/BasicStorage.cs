using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasicStorage : StorageBase
{
    public override float GetTime => throw new System.NotImplementedException();

    public override void SkillEffect()
    {
        
    }

    public override UpgradeText Upgrade()
    {
        UpgradeText res = new UpgradeText();
        switch (grade)
        {
            case 1:
                res = new UpgradeText("",
                    "");
                break;
            case 2:
                res = new UpgradeText("",
                    "");
                break;
            case 3:
                res = new UpgradeText("",
                    "");
                break;
            case 4:
                res = new UpgradeText("",
                    "");
                break;
            case 5:
                res = new UpgradeText("",
                    "");
                break;
            default:
                break;
        }

        return res;
    }
}
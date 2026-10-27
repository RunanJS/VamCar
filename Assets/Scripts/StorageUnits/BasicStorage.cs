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

    public override void Upgrade()
    {
    }

    public override UpgradeText GetExplain()
    {
        UpgradeText res = new UpgradeText();
        switch (grade)
        {
            case 0:
                res = new UpgradeText("범위 증가",
                    "공격 범위가 1.3배로 증가합니다.");
                break;
            case 1:
                res = new UpgradeText("위력 증가",
                    "공격의 위력이 130%로 증가합니다.");
                break;
            case 2:
                res = new UpgradeText("포착 대상 증가",
                    "한번에 공격하는 대상 수가 2배 증가합니다.");
                break;
            case 3:
                res = new UpgradeText("위력 증가",
                    "공격의 위력이 160%로 증가합니다.");
                break;
            case 4:
                res = new UpgradeText("범위 증가",
                    "공격 범위가 1.7배로 증가하고.");
                break;
            default:
                break;
        }

        return res;
    }
}
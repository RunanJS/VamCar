using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;

public class MixerUnit : WeaponBase
{
    public float atkSpeed;
    public float gasDuration;
    public Gas gasPrefab;

    public override float GetTime => atkSpeed; 

    public override void SkillEffect()
    {
        for (int i = 0; i < catchers.Count; i++)
        {
            Instantiate(gasPrefab, catchers[i].Position(transform), 
                Quaternion.identity).SetGas(catchers[i], this);
        }
    }

    public override void Upgrade()
    {
        switch (++grade)
        {
            case 1:
                Resize(2);
                break;
            case 2:
                power = 130;
                break;
            case 3:
                gasDuration *= 1.5f;
                break;
            case 4:
                power = 160;
                break;
            case 5:
                var origin = catchers[0];
                catchers.Add(origin.CopyShifted(new Vector3(-1.5f, 0f, 0.8f)));   // 왼쪽
                catchers.Add(origin.CopyShifted(new Vector3(1.5f, 0f, 0.8f)));   // 오른쪽
                break;
            default:
                break;
        }
    }

    public override UpgradeText GetExplain()
    {
        UpgradeText res = new UpgradeText();
        switch (grade)
        {
            case 0:
                res = new UpgradeText("크기 증가",
                    "가스의 크기가 2배로 증가합니다.");
                break;
            case 1:
                res = new UpgradeText("위력 증가",
                    "공격의 위력이 130%로 증가합니다.");
                break;
            case 2:
                res = new UpgradeText("지속 증가",
                    "가스가 1.5배 더 오래 자리에 남습니다.");
                break;
            case 3:
                res = new UpgradeText("위력 증가",
                    "공격의 위력이 160%로 증가합니다.");
                break;
            case 4:
                res = new UpgradeText("공격 개수 증가",
                    "공격의 수가 3개로 증가합니다.");
                break;
            default:
                break;
        }

        return res;
    }
}

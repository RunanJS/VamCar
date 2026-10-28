using UnityEngine;

public class Poclain : WeaponBase
{
    [SerializeField] private GameObject arm;
    [SerializeField] float atkCool = 5;

    public override float GetTime => atkCool;

    public override void SkillEffect()
    {
        foreach (var enemy in CatchAll(maxCatchCount))
        {
            enemy.OnDamaged(Power);
        }
    }

    public override void Upgrade()
    {
        switch (++grade)
        {
            case 1:
                Resize(1.3f);
                break;
            case 2:
                power = 130;
                break;
            case 3:
                maxCatchCount *= 2;
                break;
            case 4:
                power = 160;
                break;
            case 5:
                Resize(1.3f);
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

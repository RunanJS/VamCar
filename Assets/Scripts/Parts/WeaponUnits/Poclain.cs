using UnityEngine;

public class Poclain : WeaponBase
{
    [SerializeField] private GameObject arm;

    public override float GetTime => 2;

    public override void SkillEffect()
    {
        foreach (var enemy in CatchAll())
        {
            enemy.OnDamaged(Power);
        }
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

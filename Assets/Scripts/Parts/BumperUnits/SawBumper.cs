using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SawBumper : WeaponBase
{
    [SerializeField] GameObject saw;

    public override float GetTime => Time.deltaTime;

    public override void SkillEffect()
    {
        float speed = 500 * Time.deltaTime;
        saw.transform.Rotate(0, speed, 0);

        foreach (var target in CatchAll(maxCatchCount))
        {
            target.OnDamaged(Power * Time.deltaTime);
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

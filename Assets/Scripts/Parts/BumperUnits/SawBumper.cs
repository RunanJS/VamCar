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

        foreach (var target in CatchAll())
        {
            target.OnDamaged(Power * Time.deltaTime);
        }
    }

}

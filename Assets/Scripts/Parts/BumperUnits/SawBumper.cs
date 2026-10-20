using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SawBumper : WeaponBase
{
    [SerializeField] GameObject[] obj;

    public override float GetTime => Time.deltaTime;

    public override void SkillEffect()
    {
        float speed = 500 * Time.deltaTime;
        obj[0].transform.Rotate(0, speed, 0);
        obj[1].transform.Rotate(0, -speed, 0);

        foreach (var target in targets)
        {
            target.OnDamaged(power * Time.deltaTime);
        }
    }

}

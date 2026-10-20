using UnityEngine;

public class Poclain : WeaponBase
{
    [SerializeField] private GameObject arm;

    public override float GetTime => 5;

    public override void SkillEffect()
    {
        foreach (var target in targets)
        {
            target.OnDamaged(power);
        }
    }
}

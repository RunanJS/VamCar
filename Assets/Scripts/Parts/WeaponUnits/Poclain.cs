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
}

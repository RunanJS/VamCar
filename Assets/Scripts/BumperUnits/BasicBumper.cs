using UnityEngine;

public class BasicBumper : BumperBase
{
    public override bool SkillEffect(Enemy target)
    {
        target.OnDamaged();
        return true;
    }
}

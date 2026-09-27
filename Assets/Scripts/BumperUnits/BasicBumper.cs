using UnityEngine;

public class BasicBumper : BumperBase
{
    public override bool SkillEffect(Enemy target)
    {
        Shock(target);
        return true;
    }
}

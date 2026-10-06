using UnityEngine;

public class BasicBumper : BumperBase
{
    public override void SkillEffect(SkillContext context)
    {
        Shock(context.target);
    }
}

using UnityEngine;

public class BasicBumper : BumperBase
{
    public override bool IsActivated => false;
    public override float GetTime => 0;

    public override void SkillEffect()
    {
    }
}

using UnityEngine;

public class BasicBumper : BumperBase
{
    public override bool IsActivated => false;
    public override float GetTime => 0;

    public override UpgradeText GetExplain()
    {
        throw new System.NotImplementedException();
    }

    public override void SkillEffect()
    {
    }

    public override void Upgrade()
    {
        throw new System.NotImplementedException();
    }
}
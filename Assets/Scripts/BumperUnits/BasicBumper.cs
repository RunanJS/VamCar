using UnityEngine;

public class BasicBumper : BumperBase
{
    public override bool SkillEffect(Enemy target)
    {
        Vector3 force = (transform.position - target.transform.position).normalized * 4000;
        force.y = 0;
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        target.OnDamaged(power);
        return true;
    }
}

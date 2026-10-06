using UnityEngine;

public abstract class BumperBase : PartBase
{
    public float power;

    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy e))
            SkillEffect(new SkillContext { target = e });
    }

    // 이 아래로 공격 방식
    public void Scratch(Unit target)
    {
        target.OnDamaged(power * Time.deltaTime);
    }

    public void Shock(Unit target)
    {
        Vector3 force = (transform.position - target.transform.position).normalized * 4000;
        force.y = 0;
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        target.OnDamaged(power);
    }
}

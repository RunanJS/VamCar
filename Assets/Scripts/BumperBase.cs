using UnityEngine;

public abstract class BumperBase : PartBase<Enemy>
{
    public float power;

    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy e))
            SkillEffect(e);
    }

    public void Shock(Enemy target)
    {
        Vector3 force = (transform.position - target.transform.position).normalized * 4000;
        force.y = 0;
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        target.OnDamaged(power);
    }
}

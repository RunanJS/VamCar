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
}

using UnityEngine;

public abstract class BumperBase : PartBase<Enemy>
{
    public float power;

    public void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Enemy>(out Enemy e))
        SkillEffect(e);   
    }
}

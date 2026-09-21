using UnityEngine;

public abstract class BumperBase : MonoBehaviour, PartBase<Enemy>
{
    public float power;



    public abstract bool SkillEffect(Enemy target);


    public void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Enemy>(out Enemy e))
        SkillEffect(e);   
    }
}

using UnityEngine;

public abstract class EngineBase : MonoBehaviour, PartBase<GameObject>
{
    public float power;

    [SerializeField] private float enginePower;

    public float Power { get { return enginePower; } }

    public virtual bool SkillEffect(GameObject target)
    {
        return true;
    }
}

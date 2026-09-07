using UnityEngine;

public class EngineBase : MonoBehaviour, PartBase
{
    [SerializeField] private float enginePower;

    public float Power { get { return enginePower; } }

    public virtual bool SkillEffect()
    {
        return true;
    }
}

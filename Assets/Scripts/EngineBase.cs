using UnityEngine;

public class EngineBase : MonoBehaviour, PartBase<GameObject>
{
    [SerializeField] private float enginePower;

    public float Power { get { return enginePower; } }

    public virtual bool SkillEffect(GameObject target)
    {
        return true;
    }
}

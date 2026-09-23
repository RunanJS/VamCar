using UnityEngine;

public abstract class EngineBase : PartBase<GameObject>
{
    public float power;

    [SerializeField] private float enginePower;

    public float Power { get { return enginePower; } }
}

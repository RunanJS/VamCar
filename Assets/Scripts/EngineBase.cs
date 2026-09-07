using UnityEngine;

public class EngineBase : MonoBehaviour
{
    [SerializeField] private float enginePower;

    public float Power { get { return enginePower; } }
}

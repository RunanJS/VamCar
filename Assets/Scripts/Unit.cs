using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected float currentHp;
    [SerializeField] protected float maxHp;

    protected virtual void Start()
    {
        currentHp = maxHp;
    }
}

using UnityEngine;

public abstract class Enemy : Unit
{
    public GameObject target;

    private void OnEnable()
    {
        EnemyManager.Instance.Register(this);
    }

    private void OnDisable()
    {
        EnemyManager.Instance.Unregister(this);
    }

    protected abstract bool MoveRequirement { get; }

    protected virtual void Move()
    {

    }
}

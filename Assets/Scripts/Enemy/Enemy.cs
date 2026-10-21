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
        if (target == null) return;

        Vector3 dir = target.transform.position - transform.position;
        if (dir.sqrMagnitude < 0.0001f) return;   // 거리가 0이면 방향이 없음

        Quaternion look = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, look, 
            7 * Time.deltaTime);

        rb.MovePosition(transform.position + stat.speed * transform.forward * Time.deltaTime);
    }

    void Update()
    {
        if (MoveRequirement) Move();
    }
}

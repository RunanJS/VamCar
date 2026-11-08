using UnityEngine;

public abstract class Enemy : Unit
{
    public GameObject target;
    [SerializeField] ExpObject dropExp;
    [SerializeField] int dropAmount;

    private void OnEnable()
    {
        if (EnemyManager.Instance != null)
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
        dir.y = 0;
        Quaternion look = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, look, 
            7 * Time.deltaTime);

        rb.MovePosition(transform.position + stat.speed * transform.forward * Time.deltaTime);
    }

    void Update()
    {
        if (MoveRequirement) Move();
    }

    public override void DeadStart()
    {
        // Debug.Log(dropItemId + " 떨구기 " + dropItemAmount);

        Vector3 r;
        for (int i = 0; i < dropAmount; i++)
        {
            r = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            Instantiate(dropExp, transform.position + r, Quaternion.identity);
            base.DeadStart();
        }
    }
}

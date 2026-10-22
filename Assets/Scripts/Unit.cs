using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Unit : MonoBehaviour
{
    [SerializeField] protected float currentHp;
    [SerializeField] protected float maxHp;
    public Status stat;
    public bool IsDead { get; protected set; } = false;
    protected Rigidbody rb;

    protected virtual void Start()
    {
        currentHp = maxHp;
        rb = GetComponent<Rigidbody>();
    }

    public void OnDamaged(float damage)
    {
        if (IsDead) return;
        currentHp -= damage;
        if(currentHp <= 0)
        {
            DeadStart();
        }
    }

    public virtual void DeadStart()
    {
        IsDead = true;
        GetComponent<Collider>().enabled = false;
        Dead();
    }

    public virtual void Dead()
    {
        Destroy(gameObject);
    }
}

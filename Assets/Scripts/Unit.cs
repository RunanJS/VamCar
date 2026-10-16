using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Unit : MonoBehaviour
{
    [SerializeField] protected float currentHp;
    [SerializeField] protected float maxHp;
    private bool isDead = false;

    protected virtual void Start()
    {
        currentHp = maxHp;
    }

    public void OnDamaged(float damage)
    {
        currentHp -= damage;
        if(isDead && currentHp <= 0)
        {
            DeadStart();
        }
    }

    public virtual void DeadStart()
    {
        isDead = true;
        GetComponent<Collider>().enabled = false;
        Dead();
    }

    public virtual void Dead()
    {
        Destroy(gameObject);
    }
}

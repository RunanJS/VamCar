using UnityEngine;

public class Enemy : Unit
{
    public void OnDamaged(float damage)
    {
        currentHp -= damage;
    }
}

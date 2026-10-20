using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : PartBase
{
    public float power;
    public List<Enemy> targets = new List<Enemy>();

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            targets.Add(enemy);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            targets.Remove(enemy);
        }
    }

}

using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : PartBase
{
    public float power = 100;
    public List<Enemy> targets = new List<Enemy>();

    public float Power => CarManager.Inst.Stat.atk * power / 100;

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

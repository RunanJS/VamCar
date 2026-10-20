using UnityEngine;

public class BasicEnemy : Enemy
{
    protected override bool MoveRequirement =>
        Vector3.Distance(target.transform.position, transform.position) < 50;
}

using UnityEngine;

public class BasicEnemy : Enemy
{
    [SerializeField] TargetCatcher catcher;
    float timer = 0;

    protected override bool MoveRequirement =>
        Vector3.Distance(target.transform.position, transform.position) < 50;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.1f)
        {
            timer = 0;
            foreach (var item in catcher.Catch(transform))
            {
                item.OnDamaged(stat.atk * 0.1f);
            }
        }
    }

    protected virtual void OnDrawGizmos()
    {
        if (catcher == null) return;
        catcher.DrawGizmos(transform);
    }
}

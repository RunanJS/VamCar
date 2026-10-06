using UnityEngine;

public class BasicGun : WeaponBase, IUpdated
{
    private float timer;
    [SerializeField] private float rapidSpeed = 3;
    [SerializeField] private float range = 25;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private GameObject bullet;

    public void OnUpdate()
    {
        timer -= Time.deltaTime;
        if(timer < rapidSpeed)
        {
            SkillEffect(new SkillContext
            {
                target =
                    EnemyManager.Instance.GetNearestEnemy(
                        transform.position)
            });
            timer = rapidSpeed;
        }
    }

    public override void SkillEffect(SkillContext context)
    {
        Destroy(Instantiate(bullet, transform.position,
            Quaternion.LookRotation(transform.position,
            context.target.transform.position)), 5);
    }
}

using UnityEngine;

public class BasicGun : WeaponBase, IUpdated
{
    private float timer;
    [SerializeField] private float bulletSpeed = 7;
    [SerializeField] private float rapidSpeed = 3;
    [SerializeField] private float range = 25;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePos;

    public void OnUpdate()
    {
        timer += Time.deltaTime;
        if(timer > rapidSpeed)
        {
            SkillEffect(new SkillContext
            {
                target =
                    EnemyManager.Instance.GetNearestEnemy(
                        transform.position)
            });
            timer = 0;
        }
    }

    public override void SkillEffect(SkillContext context)
    {
        Instantiate(bullet, firePos.position, transform.rotation)
            .GetComponent<Bullet>().SetBullet(
            bulletSpeed, power, context.target.transform);
    }
}

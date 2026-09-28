using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SawBumper : PartBase
{
    [SerializeField] GameObject[] obj;
    public float power;

    void Update()
    {
        float speed = 500 * Time.deltaTime;
        obj[0].transform.Rotate(0, speed, 0);
        obj[1].transform.Rotate(0, -speed, 0);
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
        if (other.TryGetComponent<Unit>(out Unit target))
        {
            SkillEffect(new SkillContext { target = target });
        }
    }

    public override void SkillEffect(SkillContext context)
    {
        context.target.OnDamaged(power * Time.deltaTime);
    }
}

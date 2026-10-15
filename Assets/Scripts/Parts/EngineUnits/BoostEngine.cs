using UnityEngine;

public class BoostAddOn : AddOnBase, ISpeed, IUpdated
{
    [SerializeField] private float driftGage = 0;
    public float driftActiveValue = 3;
    public float driftPower;

    public float Power { get { return driftPower; } }

    public void OnUpdate()
    {
        if (InputManager.Brake && InputManager.Move != Vector2.zero)
        {
            driftGage += Time.deltaTime;
        }
        else
        {
            driftGage = Mathf.Max(0, driftGage - Time.deltaTime);
        }

        if(driftGage > driftActiveValue)
        {

        }
    }

    public override void SkillEffect(SkillContext context)
    {
        throw new System.NotImplementedException();
    }
}

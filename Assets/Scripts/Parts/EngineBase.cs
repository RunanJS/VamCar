using UnityEngine;

public abstract class EngineBase : PartBase
{
    public float generateAmount;
    public float generateTime;
    [SerializeField] private float enginePower;

    protected float generateValue = 1;
    protected bool isFuelOn = false;

    public override float GetTime => generateTime;

    protected abstract bool GetRequired { get; }
    public override bool IsActivated
    {
        get
        {
            if (!isFuelOn)
            {
                if (GetRequired)
                {
                    isFuelOn = true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }

    public float Power
    {
        get
        {
            if (IsActivated)
                return enginePower;
            else 
                return 0;
        }
    }

    public float Generate()
    {
        if (isFuelOn)
        {
            if (timer < 0)
            {
                isFuelOn = false;
                return generateAmount;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            if (GetRequired)
            {
                isFuelOn = true;
                timer = generateTime;
            }
        }
        return 0;
    }

    public override void SkillEffect()
    {
        CarManager.Inst.exp += generateAmount;
        isFuelOn = false;
    }
}
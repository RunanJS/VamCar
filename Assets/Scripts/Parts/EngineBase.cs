using UnityEngine;

public abstract class EngineBase : PartBase
{
    public float generateAmount;
    public float generateTime;
    [SerializeField] private float enginePower;

    [Header("작동 중 여부")]
    protected bool isActivated = false;


    float timer = 0;

    protected abstract bool GetRequired { get; }

    public float Power
    {
        get
        {
            if (isActivated)
                return enginePower;
            else 
                return 0;
        }
    }

    public float Generate()
    {
        if (isActivated)
        {
            if (timer < 0)
            {
                isActivated = false;
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
                isActivated = true;
                timer = generateTime;
            }
        }
        return 0;
    }
}
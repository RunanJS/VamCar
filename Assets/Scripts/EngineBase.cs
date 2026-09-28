using UnityEngine;

public abstract class EngineBase : PartBase
{
    public float maxFuel;
    public float fuel;
    public float generateAmount;
    public float generateTime;

    [Header("작동 중 여부")]
    protected bool isActivated = false;

    [SerializeField] private float enginePower;

    

    float timer = 0;

    protected abstract float Require { get; }

    public float Power
    {
        get
        {
            if (fuel > 0)
                return enginePower;
            else 
                return 0;
        }
    }

    public float GetEnergy(float requierd)
    {
        if (fuel < requierd) return 0;

        fuel -= requierd;
        return requierd;
    }

    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = generateTime;
            Generate();
        }
    }

    public void Generate()
    {
        if (fuel >= maxFuel) return;

        fuel += Require;
    }
}

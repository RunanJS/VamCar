using UnityEngine;

[System.Serializable]
public struct Status
{
    public float atk;
    public float speed;
    public float critChance;
    public float critAtk;
    public float resistance;

    public Status(float atk, float speed,
        float critChance, float critAtk, float resistance)
    {
        this.atk = atk;
        this.speed = speed;
        this.critChance = critChance;
        this.critAtk = critAtk;
        this.resistance = resistance;
    }
}
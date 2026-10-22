using UnityEngine;

[System.Serializable]
public abstract class PartBase : MonoBehaviour
{
    public float timer;
    public abstract float GetTime { get; }
    public virtual bool IsActivated { get; } = true;

    public abstract void SkillEffect();
}

public class SkillContext
{
    public Unit target;
    public float duration;
}
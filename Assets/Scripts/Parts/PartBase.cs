using UnityEngine;

public abstract class PartBase : MonoBehaviour
{
    public abstract void SkillEffect(SkillContext context);
}

public class SkillContext
{
    public Unit target;
    public float duration;
}
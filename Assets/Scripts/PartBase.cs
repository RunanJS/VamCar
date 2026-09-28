using UnityEngine;

public abstract class PartBase : MonoBehaviour
{
    public int partSize = 1;

    [Header("상하전후좌우 2진수")]
    public int slotable = 0b111111;

    public abstract void SkillEffect(SkillContext context);
}

public class SkillContext
{
    public Unit target;
    public float duration;
}
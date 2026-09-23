using UnityEngine;

public abstract class PartBase<T> : MonoBehaviour
{
    public int size = 1;

    [Header("상하전후좌우 2진수")]
    [Header("상하전후좌우 2진수")]
    public int slotable = 0b111111;

    public abstract bool SkillEffect(T t);
}
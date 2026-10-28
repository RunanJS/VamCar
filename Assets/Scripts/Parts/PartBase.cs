using UnityEngine;

[System.Serializable]
public abstract class PartBase : MonoBehaviour
{
    public float timer;
    public int grade = 5;
    public int MaxGrade;

    public bool Upgradeable => grade < MaxGrade;
    public abstract float GetTime { get; }
    public virtual bool IsActivated { get; } = true;

    public abstract void SkillEffect();

    // 설명문을 반환
    public abstract void Upgrade();
    public abstract UpgradeText GetExplain();
}

public class SkillContext
{
    public Unit target;
    public float duration;
}

public struct UpgradeText
{
    public string title;
    public string line;

    public UpgradeText(string title, string line)
    {
        this.title = title;
        this.line = line;
    }
}
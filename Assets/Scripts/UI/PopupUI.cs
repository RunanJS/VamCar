using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PopupUI : MonoBehaviour
{
    CanvasGroup group;

    public bool IsOpen { get; private set; }

    void Awake()
    {
        Init();
        Close();
    }

    void Init()
    {
        if (group == null) group = GetComponent<CanvasGroup>();
    }

    public void Open() => Set(true);
    public void Close() => Set(false);
    public void Toggle() => Set(!IsOpen);

    void Set(bool open)
    {
        Init();
        IsOpen = open;
        group.alpha = open ? 1f : 0f;
        group.interactable = open;
        group.blocksRaycasts = open;
    }
}
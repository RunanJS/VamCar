using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    
    void OnEnable() => InputManager.OnInventoryToggle += Toggle;
    void OnDisable() => InputManager.OnInventoryToggle -= Toggle;

    void Toggle()
    {
        canvas.enabled = !canvas.enabled;
    }
}

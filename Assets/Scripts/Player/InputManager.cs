using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Move;
    public static bool Brake;
    public static event Action OnInventoryToggle;

    // PlayerInput 컴포넌트(Behavior: Send Messages)가 호출
    void OnMove(InputValue value)
    {
        Move = value.Get<Vector2>();
        Debug.Log(value.ToString());
    }
    void OnBrake(InputValue value)
    {
        Brake = value.isPressed;
    }
    void OnInventory(InputValue value)
    {
        if (value.isPressed) OnInventoryToggle?.Invoke();
    }

    void OnDisable()
    {
        Move = Vector2.zero;
        Brake = false;
    }
}
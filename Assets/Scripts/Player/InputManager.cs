using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Move;
    public static bool Brake;
    public static event Action OnMenuToggle;
    public static event Action OnEscOut;
    public static event Action OnPressAnyKey;

    // PlayerInput 컴포넌트(Behavior: Send Messages)가 호출
    void OnMove(InputValue value)
    {
        Move = value.Get<Vector2>();
    }
    void OnBrake(InputValue value)
    {
        Brake = value.isPressed;
    }
    void OnMenu(InputValue value)
    {
        if (value.isPressed) OnMenuToggle?.Invoke();
    }
    void OnEsc(InputValue value)
    {
        if (value.isPressed) OnEscOut?.Invoke();
    }
    void OnAnyKey(InputValue value)
    {
        if (value.isPressed) OnPressAnyKey.Invoke();
    }


    void OnDisable()
    {
        Move = Vector2.zero;
        Brake = false;
    }
}
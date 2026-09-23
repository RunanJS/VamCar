using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("바퀴")]
    [SerializeField] WheelCollider wheelFL;
    [SerializeField] WheelCollider wheelFR;
    [SerializeField] WheelCollider wheelRL;
    [SerializeField] WheelCollider wheelRR;

    public CarManager carManager;

    public float MotorTorque
    {
        get
        {
            return carManager.EnginePower * 1000f;
        }
    }
    public float maxSteerAngle = 30f;

    private Vector2 moveInput;
    Rigidbody rb;

    // public bool isDrift = false;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    /*
    public void OnDrift(InputAction.CallbackContext context)
    {
        isDrift = context.performed;
    }*/

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
        carManager = GetComponent<CarManager>();
    }

    private void FixedUpdate()
    {
        // Debug.Log(moveInput);
        // W/S
        float motor = moveInput.y * MotorTorque;

        wheelRL.motorTorque = motor;
        wheelRR.motorTorque = motor;

        // A/D
        float steer = moveInput.x * maxSteerAngle;

        wheelFL.steerAngle = steer;
        wheelFR.steerAngle = steer;

        // 드리프트
        // SetDrift(isDrift);
    }

    /*
    private void SetDrift(bool drift)
    {
        float stiffness = drift ? 0.5f : 1.0f;

        SetSidewaysFriction(wheelFL, stiffness);
        SetSidewaysFriction(wheelFR, stiffness);
        SetSidewaysFriction(wheelRL, stiffness);
        SetSidewaysFriction(wheelRR, stiffness);
    }

    private void SetSidewaysFriction(WheelCollider wheel, float stiffness)
    {
        WheelFrictionCurve friction = wheel.sidewaysFriction;
        friction.stiffness = stiffness;
        wheel.sidewaysFriction = friction;
    }*/


}

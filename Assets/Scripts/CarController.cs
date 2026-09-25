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

    public float engineBrakeTorque = 500;
    public bool isBrake = false;

    private Vector2 moveInput;
    Rigidbody rb;

    // public bool isDrift = false;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnBrake(InputAction.CallbackContext context)
    {
        isBrake = context.performed;
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
        // 브레이크시 입력 씹음
        float input = isBrake ? 0 : moveInput.y;

        // W/S
        float motor = input * MotorTorque;

        // 엑셀을 놓았을 때 엔진 브레이크
        if (Mathf.Abs(input) < 0.01f)
        {
            float speed = Vector3.Dot(rb.linearVelocity, transform.forward);

            float brakePower = isBrake ? 3 : 1;
            motor = -Mathf.Sign(speed) * engineBrakeTorque * brakePower;
        }

        wheelRL.motorTorque = motor;
        wheelRR.motorTorque = motor;

        // A/D
        float steer = moveInput.x * maxSteerAngle;

        wheelFL.steerAngle = steer;
        wheelFR.steerAngle = steer;

        // 드리프트
        SetDrift(isBrake);
    }

    
    private void SetDrift(bool drift)
    {
        float stiffness = drift ? 0.3f : 1.0f;

        //SetSidewaysFriction(wheelFL, stiffness);
        //SetSidewaysFriction(wheelFR, stiffness);
        SetSidewaysFriction(wheelRL, stiffness);
        SetSidewaysFriction(wheelRR, stiffness);
    }

    private void SetSidewaysFriction(WheelCollider wheel, float stiffness)
    {
        WheelFrictionCurve friction = wheel.sidewaysFriction;
        friction.stiffness = stiffness;
        wheel.sidewaysFriction = friction;
    }


}

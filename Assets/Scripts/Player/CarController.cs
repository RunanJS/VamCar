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
    public bool IsBrake => InputManager.Brake;

    private Vector2 MoveInput => InputManager.Move;
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
        carManager = GetComponent<CarManager>();
    }

    private void FixedUpdate()
    {
        // 브레이크시 입력 씹음
        float input = IsBrake ? 0 : MoveInput.y;

        // W/S
        float motor = input * MotorTorque;

        // 엑셀을 놓았을 때 엔진 브레이크
        if (Mathf.Abs(input) < 0.01f)
        {
            float speed = Vector3.Dot(rb.linearVelocity, transform.forward);

            float brakePower = IsBrake ? 3 : 1;
            motor = -Mathf.Sign(speed) * engineBrakeTorque * brakePower;
        }

        wheelRL.motorTorque = motor;
        wheelRR.motorTorque = motor;

        // A/D
        float steer = MoveInput.x * maxSteerAngle;

        wheelFL.steerAngle = steer;
        wheelFR.steerAngle = steer;

        // 드리프트
        SetDrift(IsBrake);
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

using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : Unit
{
    [Header("바퀴")]
    [SerializeField] WheelCollider wheelFL;
    [SerializeField] WheelCollider wheelFR;
    [SerializeField] WheelCollider wheelRL;
    [SerializeField] WheelCollider wheelRR;

    public CarManager carManager;

    public bool IsBrake => InputManager.Brake;

    private Vector2 MoveInput => InputManager.Move;


    protected override void Start()
    {
        base.Start();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
        carManager = GetComponent<CarManager>();
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        float x = MoveInput.x;
        float y = MoveInput.y;

        if (IsBrake)
        {
            x = 0;
            y = 0;
        }

        rb.MovePosition(transform.position + transform.forward 
            * y * stat.speed *  carManager.EnginePower * Time.deltaTime);

        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f,
            x * 90f * Time.deltaTime, 0f));

        /*
        // 브레이크시 입력 씹음
        float input = IsBrake ? 0 : y;

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
        float steer = x * maxSteerAngle;

        wheelFL.steerAngle = steer;
        wheelFR.steerAngle = steer;

        // 드리프트
        SetDrift(IsBrake);

        if(MoveInput.y == 0)
        {
            wheelRL.motorTorque = -motor;
            wheelFL.motorTorque = -motor;
            wheelRR.motorTorque = motor;
            wheelFR.motorTorque = motor;
        }*/
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

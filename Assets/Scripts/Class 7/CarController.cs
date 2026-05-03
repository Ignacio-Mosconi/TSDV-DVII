using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Serializable]
    public class AxleData
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool isPowered;
        public bool isSteerable;
    }

    [SerializeField] private AxleData[] axles;
    [SerializeField] private Transform centerOfMass;
    [SerializeField] private Transform cameraFocusPoint;
    [SerializeField, Range(0f, 1000f)] private float maxMotorTorque;
    [SerializeField, Range(0f, 180f)] private float maxSteeringAngle;

    private new Rigidbody rigidbody;

    
    void Awake ()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = centerOfMass.localPosition;

        CameraTracker cameraTracker = FindAnyObjectByType<CameraTracker>();

        if (cameraTracker)
            cameraTracker.SetFollowTarget(cameraFocusPoint);
    }

    void FixedUpdate ()
    {
        float motorTorque = maxMotorTorque * Input.GetAxis("Vertical");
        float steeringAngle = maxSteeringAngle* Input.GetAxis("Horizontal");

        foreach (AxleData axle in axles)
        {
            if (axle.isPowered)
            {
                axle.leftWheel.motorTorque = motorTorque;
                axle.rightWheel.motorTorque = motorTorque;
            }

            if (axle.isSteerable)
            {
                axle.leftWheel.steerAngle = steeringAngle;
                axle.rightWheel.steerAngle = steeringAngle;
            }

            UpdateWheelVisuals(axle.leftWheel);
            UpdateWheelVisuals(axle.rightWheel);
        }

        rigidbody.AddForce(-transform.up * rigidbody.linearVelocity.magnitude);
    }

    private void UpdateWheelVisuals (WheelCollider wheelCollider)
    {
        if (wheelCollider.transform.childCount == 0)
            return;

        Transform wheelGraphic = wheelCollider.transform.GetChild(0);
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);
        wheelGraphic.SetPositionAndRotation(position, rotation);
    }
}

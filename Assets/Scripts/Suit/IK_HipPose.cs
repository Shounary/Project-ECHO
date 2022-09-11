using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK_HipPose : MonoBehaviour
{
    [SerializeField] private Transform hipTransform, cameraTransform;
    [SerializeField] private Vector3 offsetVector;
    [SerializeField] private float hipTurnThreshold = 0.2f;
    [SerializeField] private float turnSensitivity = 1f;


    private void Awake()
    {
        hipTransform.position = cameraTransform.position + offsetVector;
        Vector3 newForward = Vector3.ProjectOnPlane((cameraTransform.up + cameraTransform.forward).normalized, Vector3.up).normalized;

        if (Vector3.Distance(newForward, hipTransform.forward) > hipTurnThreshold) {
            hipTransform.forward = Vector3.Lerp(hipTransform.forward, newForward, turnSensitivity * Time.deltaTime);
        }
    }

    void Update()
    {
        hipTransform.position = cameraTransform.position + offsetVector;
        Vector3 newForward = Vector3.ProjectOnPlane((cameraTransform.up + cameraTransform.forward).normalized, Vector3.up).normalized;

        if (Vector3.Distance(newForward, hipTransform.forward) > hipTurnThreshold) {
            hipTransform.forward = Vector3.Lerp(hipTransform.forward, newForward, turnSensitivity * Time.deltaTime);
        }
    }
}

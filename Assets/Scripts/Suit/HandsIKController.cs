using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandsIKController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float targetDistanceToShoulder = 1.5f;
    [SerializeField] private float trackSmoothness = 0.5f;

    [Header("References")]
    [SerializeField] private InputActionReference rightFiringModeToggleReference;
    [SerializeField] private Transform rightShoulder;
    [SerializeField] private Transform rightControllerTarget;
    [SerializeField] private Transform rightIKTarget;

    [SerializeField] private InputActionReference leftFiringModeToggleReference;
    [SerializeField] private Transform leftShoulder;
    [SerializeField] private Transform leftControllerTarget;
    [SerializeField] private Transform leftIKTarget;

    private bool isRightFiringModeOn = false;
    private bool isLeftFiringModeOn = false;

    void Start()
    {
        rightFiringModeToggleReference.action.performed += ToggleRightFiringMode;
        leftFiringModeToggleReference.action.performed += ToggleLeftFiringMode;
    }

    void LateUpdate()
    {
        if (isRightFiringModeOn) {
            TrackRightIKWithOffset();
        } else {
            TrackRightIK();
        }

        if (isLeftFiringModeOn) {
            TrackLeftIKWithOffset();
        } else {
            TrackLeftIK();
        }
    }

    private void ToggleRightFiringMode(InputAction.CallbackContext context)
    {
        isRightFiringModeOn = !isRightFiringModeOn;
    }

    private void ToggleLeftFiringMode(InputAction.CallbackContext context)
    {
        isLeftFiringModeOn = !isLeftFiringModeOn;
    }


    // Right Hand
    private void TrackRightIKWithOffset()
    {
        rightIKTarget.rotation = rightControllerTarget.rotation;
        rightIKTarget.position = Vector3.Lerp(rightIKTarget.position,
            rightControllerTarget.position + (rightControllerTarget.position - rightShoulder.position).normalized * targetDistanceToShoulder,
            trackSmoothness * 1.5f);
    }

    private void TrackRightIK()
    {
        rightIKTarget.rotation = rightControllerTarget.rotation;
        rightIKTarget.position = Vector3.Lerp(rightIKTarget.position, rightControllerTarget.position, trackSmoothness);
    }


    // Left Hand
    private void TrackLeftIKWithOffset()
    {
        leftIKTarget.rotation = leftControllerTarget.rotation;
        leftIKTarget.position = Vector3.Lerp(leftIKTarget.position,
            leftControllerTarget.position + (leftControllerTarget.position - leftShoulder.position).normalized * targetDistanceToShoulder,
            trackSmoothness * 1.5f);
    }

    private void TrackLeftIK()
    {
        leftIKTarget.rotation = leftControllerTarget.rotation;
        leftIKTarget.position = Vector3.Lerp(leftIKTarget.position, leftControllerTarget.position, trackSmoothness);
    }


}

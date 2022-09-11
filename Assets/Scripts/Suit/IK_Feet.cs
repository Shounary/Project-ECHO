using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK_Feet : MonoBehaviour
{
    [SerializeField] private LayerMask groundSurfaceMask;
    [Range(0, 1)]
    [SerializeField] private float rightFootPosWeight = 1f;
    [Range(0, 1)]
    [SerializeField] private float rightFootRotWeight = 1f;
    [Range(0, 1)]
    [SerializeField] private float leftFootPosWeight = 1f;
    [Range(0, 1)]
    [SerializeField] private float leftFootRotWeight = 1f;
    [SerializeField] private Vector3 posOffset = new Vector3(0f, 0.15f, 0f);
    [SerializeField] private float raycastDepth = 1.2f;

    [SerializeField] private Animator animator;
    [SerializeField] private FeetMovement feetMovement;

    void OnAnimatorIK()
    {
        Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);

        RaycastHit hit;
        if (Physics.Raycast(Vector3.up + rightFootPos, Vector3.down, out hit, raycastDepth, groundSurfaceMask)) {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPosWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootRotWeight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + posOffset);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal));
            feetMovement.enabled = true;
        } else {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0f);
            feetMovement.enabled = false;
        }

        Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);

        RaycastHit hitLeft;
        if (Physics.Raycast(Vector3.up + leftFootPos, Vector3.down, out hitLeft, raycastDepth, groundSurfaceMask)) {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPosWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootRotWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hitLeft.point + posOffset);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal));
            feetMovement.enabled = true;
        } else {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0f);
            feetMovement.enabled = false;
        }
    }
}

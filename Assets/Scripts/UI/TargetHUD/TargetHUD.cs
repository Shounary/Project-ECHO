using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHUD : MonoBehaviour
{
    [SerializeField] private GameObject targetHUD;
    [SerializeField] private float maxRange = 50f;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask ignoreLayers;

    [SerializeField] private float targetHUDOffsetFactor = 0.2f;

    void FixedUpdate()
    {
        Vector3 targetToCameraVector = mainCamera.position - targetPoint.position;

        RaycastHit hit;
        if (Physics.Raycast(targetPoint.position, targetToCameraVector, out hit, maxRange, ~(ignoreLayers))) {
            if (playerLayer == (playerLayer | (1 << hit.collider.gameObject.layer))) {
                targetHUD.transform.forward = targetToCameraVector.normalized;
                targetHUD.transform.position = targetPoint.position + targetToCameraVector * targetHUDOffsetFactor;
                targetHUD.SetActive(true);
            } else {
                targetHUD.SetActive(false);
            }
        }
    }
}

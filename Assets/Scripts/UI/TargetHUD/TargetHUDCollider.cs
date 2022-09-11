using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHUDCollider : MonoBehaviour
{
    [SerializeField] private LayerMask environmentLayers;
    [SerializeField] private Animator hudAnimator;
    

    private void OnTriggerEnter(Collider other)
    {
        if (environmentLayers != (environmentLayers | (1 << other.gameObject.layer)))
            return;

        hudAnimator.SetBool("isDisabled", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (environmentLayers != (environmentLayers | (1 << other.gameObject.layer)))
            return;

        hudAnimator.SetBool("isDisabled", false);
    }
}

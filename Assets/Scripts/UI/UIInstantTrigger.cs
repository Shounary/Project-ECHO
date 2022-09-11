using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInstantTrigger : MonoBehaviour
{
    [SerializeField] protected LayerMask playerHandLayer;
    protected bool isActive;


    void Start()
    {
        isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerHandLayer != (playerHandLayer | (1 << other.gameObject.layer)))
            return;

        if (!isActive) {
            TriggerEnterEvent();
            isActive = true;
        }
    }

    protected virtual void TriggerEnterEvent()
    {

    }
}

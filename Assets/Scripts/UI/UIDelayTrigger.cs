using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDelayTrigger : MonoBehaviour
{
    [SerializeField] protected LayerMask playerHandLayer;
    [SerializeField] protected float delayTime = 3f;

    protected bool isActive;
    protected float counter;

    void Start()
    {
        isActive = false;
        counter = delayTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerHandLayer != (playerHandLayer | (1 << other.gameObject.layer)))
            return;

        if (!isActive) {
            TriggerEnterEvent();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerHandLayer != (playerHandLayer | (1 << other.gameObject.layer)))
            return;

        if (!isActive && counter <= 0f) {
            TriggerActivateEvent();
            isActive = true;
        } else if (!isActive) {
            counter -= Time.deltaTime;
            TriggerStayEvent();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerHandLayer != (playerHandLayer | (1 << other.gameObject.layer)))
            return;

        if (!isActive) {
            TriggerExitEvent();
        }
    }

    protected virtual void Reset()
    {
        counter = delayTime;
    }

    protected virtual void TriggerActivateEvent()
    {

    }
    protected virtual void TriggerEnterEvent()
    {

    }

    protected virtual void TriggerStayEvent()
    {

    }

    protected virtual void TriggerExitEvent()
    {
        Reset();
    }
}

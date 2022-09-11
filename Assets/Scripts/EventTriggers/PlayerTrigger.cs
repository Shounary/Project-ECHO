using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour, IExecutable
{
    [SerializeField] protected LayerMask playerLayer;
    protected bool isActive;

    void Start()
    {
        isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerLayer != (playerLayer | (1 << other.gameObject.layer)))
            return;

        if (!isActive) {
            TriggerEvent();
            isActive = true;
        }
    }

    protected virtual void TriggerEvent()
    {

    }

    public virtual void Execute()
    {
        TriggerEvent();
    }
}

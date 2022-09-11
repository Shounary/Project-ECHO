using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseTrigger : PlayerTrigger
{
    [SerializeField] protected Animator animator;

    protected override void TriggerEvent()
    {
        base.TriggerEvent();
        animator.SetBool("isOpen", false);
    }
}

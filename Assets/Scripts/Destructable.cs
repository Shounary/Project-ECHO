using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : Entity
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject destructionVFX;
    [SerializeField] protected Transform vFXtransform;

    private void Update()
    {
        if (animator.GetBool("isDestroyed"))
            return;
        if (health <= 0) {
            OnDestruction();
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (health <= 0) {
            OnDestruction();
        }
    }

    public virtual void OnDestruction()
    {
        if (animator != null) {
            animator.SetBool("isDestroyed", true);
        }

        if (destructionVFX != null) {
            Destroy(Instantiate(destructionVFX, vFXtransform.position, vFXtransform.rotation), 1.5f);
        }

        Destroy(gameObject, 1f);
    }
}

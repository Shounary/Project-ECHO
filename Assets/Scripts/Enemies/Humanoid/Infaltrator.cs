using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infaltrator : HumanoidEnemy
{
    [SerializeField] private Transform aimPoint;
    private float counter;


    void Start()
    {
        counter = 1 / enemyFireRate;
    }

    void Update()
    {
        if (animator.GetBool("isDead")) {
            return;
        }

        if (target.position - transform.position != Vector3.zero)
            transform.forward = Vector3.ProjectOnPlane(target.position - transform.position, Vector3.up);

        RaycastHit hit;
        bool inLineOfFire = Physics.Raycast(aimPoint.position, target.position + targetOffset / 2.4f - aimPoint.position, out hit, enemyAttackRange, ~(ignoreLayers));
        if (counter <= 0f) {
            if (inLineOfFire && targetLayers == (targetLayers | (1 << hit.collider.gameObject.layer))) {
                Fire();
                counter = 1 / enemyFireRate;
                animator.SetBool("isFiring", true);
            }
        } else {
            counter -= Time.deltaTime;
            animator.SetBool("isFiring", false);
        }
    }
}

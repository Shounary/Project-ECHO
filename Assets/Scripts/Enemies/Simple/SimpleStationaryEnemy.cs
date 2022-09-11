using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleStationaryEnemy : SimpleEnemy
{
    [SerializeField] protected Transform target;
    [SerializeField] protected Vector3 targetOffset;

    private float counter;

    void Start()
    {
        counter = 1 / enemyFireRate;
    }

    void Update()
    {
        transform.LookAt(target.position + targetOffset);

        RaycastHit hit;
        bool inLineOfFire = Physics.Raycast(firePoints[0].position, target.position - firePoints[0].position, out hit, enemyAttackRange, ~(ignoreLayers));
        if (counter <= 0f) {
            if (inLineOfFire && targetLayers == (targetLayers | (1 << hit.collider.gameObject.layer))) {
                Fire();
                counter = 1 / enemyFireRate;
            }
        } else {
            counter -= Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SImpleMovingEnemy : SimpleEnemy
{
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Transform target;
    [SerializeField] protected Vector3 targetOffset;

    [SerializeField] protected AudioSource flyAudio;

    private float counter;

    void Start()
    {
        counter = 1 / enemyFireRate;
    }

    protected virtual void Update()
    {
        transform.LookAt(target.position + targetOffset);

        FollowTarget();

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

    protected virtual void FollowTarget()
    {
        RaycastHit hit;
        bool inLineOfFire = Physics.Raycast(firePoints[0].position, target.position - firePoints[0].position, out hit, enemyAttackRange, ~(ignoreLayers));
        if (inLineOfFire && targetLayers == (targetLayers | (1 << hit.collider.gameObject.layer))) {
            navMeshAgent.isStopped = true;
        } else {
            navMeshAgent.SetDestination(target.position + targetOffset);
            navMeshAgent.isStopped = false;
        }
    }

    private void OnEnable()
    {
        if (flyAudio != null) {
            flyAudio.Play();
        }
    }
}

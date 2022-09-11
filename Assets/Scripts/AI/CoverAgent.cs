using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CoverAgent : MonoBehaviour
{
    [Range(0, 25)]
    [SerializeField] private float minCoverTime = 1.5f;
    [Range(0, 25)]
    [SerializeField] private float maxCoverTime = 3.5f;

    [HideInInspector]
    public CoverNode occupiedNode { get; set; }

    [HideInInspector]
    public NavMeshAgent navAgent;

    private bool inCover;
    private float counter;

    void Awake()
    {
        counter = Random.Range(minCoverTime, maxCoverTime);
        inCover = true;
        occupiedNode = null;
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        counter -= Time.deltaTime;

        if (counter <= 0) {
            if (occupiedNode != null && occupiedNode.firePositions.Length > 0 && navAgent.enabled) {
                if (inCover) {
                    navAgent.SetDestination(occupiedNode.firePositions[Random.Range(0, occupiedNode.firePositions.Length)].position);
                } else {
                    navAgent.SetDestination(occupiedNode.coverPosition.position);
                }

                inCover = !inCover;
            }

            counter = Random.Range(minCoverTime, maxCoverTime);
        }
    }


}

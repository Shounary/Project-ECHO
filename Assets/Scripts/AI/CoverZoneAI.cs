using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class CoverZoneAI : MonoBehaviour
{
    [Range(0, 25)]
    [SerializeField] private float minReplanTime = 5f;
    [Range(0, 25)]
    [SerializeField] private float maxReplanTime = 10f;

    [SerializeField] private Transform target;
    [SerializeField] private CoverAgent[] coverAgents;
    [SerializeField] private CoverNode[] coverNodes;

    private List<CoverNode> freeNodes;
    private List<CoverAgent> currentAgents;

    private float counter;


    void Start()
    {
        //  ================================================= USE Trigger to Jumpstart ===============================================
        Initiate();           
    }

    public void Initiate()
    {
        counter = RandomCounterValue(minReplanTime, maxReplanTime);
        currentAgents = new List<CoverAgent>(coverAgents);
        freeNodes = new List<CoverNode>();

        foreach (CoverNode coverNode in coverNodes) {
            if (!coverNode.isOccupied) {
                freeNodes.Add(coverNode);
            }
        }

        foreach (CoverAgent coverAgent in currentAgents) {
            AssignRandomNode(coverAgent);
        }
    }

    void Update()
    {
        counter -= Time.deltaTime;
        if (counter <= 0) {
            CoverAgent coverAgent = GetRandomAgent();
            if (coverAgent == null)
                return;

            FreeNode(coverAgent);
            AssignRandomNode(coverAgent);

            counter = RandomCounterValue(minReplanTime, maxReplanTime);
        }
    }

    float RandomCounterValue(float min, float max)
    {
        return Random.Range(min, max);
    }

    void FreeNode(CoverAgent coverAgent)
    {
        if (coverAgent.occupiedNode == null) {
            return;
        }

        coverAgent.occupiedNode.isOccupied = false;
        freeNodes.Add(coverAgent.occupiedNode);
        coverAgent.occupiedNode = null;
    }

    void AssignRandomNode(CoverAgent coverAgent)
    {
        CoverNode coverNode = freeNodes[Random.Range(0, freeNodes.Count)];
        if (coverNode == null)
            return;

        coverNode.isOccupied = true;
        freeNodes.Remove(coverNode);
        coverAgent.occupiedNode = coverNode;

        if (coverAgent.navAgent.enabled)
            coverAgent.navAgent.SetDestination(coverNode.gameObject.transform.position);
    }

    CoverAgent GetRandomAgent()
    {
        CoverAgent coverAgent = null;
        while (coverAgent == null && currentAgents.Count > 0) {
            int i = Random.Range(0, currentAgents.Count);
            coverAgent = currentAgents[i];
            if (coverAgent == null) {
                currentAgents.RemoveAt(i);
            }
        }

        return coverAgent;
    }
}

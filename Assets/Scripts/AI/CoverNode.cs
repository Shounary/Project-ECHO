using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverNode : MonoBehaviour
{
    public Transform[] firePositions;
    public Transform coverPosition { get; set; }

    [HideInInspector]
    public bool isOccupied { get; set; }

    [HideInInspector]
    public bool isExposed { get; set; }

    void Start()
    {
        coverPosition = transform;
        isOccupied = false;
        isExposed = false;
    }
}

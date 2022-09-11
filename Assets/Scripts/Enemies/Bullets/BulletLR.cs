using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BulletLR : Bullet
{
    [SerializeField] protected float bulletLength = 0.5f;

    private LineRenderer lr;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + transform.forward * bulletLength);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanatos : WeaponPlatform
{
    [SerializeField] protected Transform target;
    [SerializeField] protected Vector3 targetOffset;

    private List<float> counters;

    void Start()
    {
        counters = new List<float>(4);
        for (int i = 0; i < weapons.Length; i++) {
            counters.Add(1 / weapons[i].fireRate);
        }
    }

    protected virtual void Update()
    {
        transform.LookAt(target.position + targetOffset);

        for (int i = 0; i < weapons.Length; i++) {
            RaycastHit hit;
            bool inLineOfFire = Physics.Raycast(weapons[i].firePoints[0].position, target.position - weapons[i].firePoints[0].position, out hit, weapons[i].attackRange, ~(ignoreLayers));
            inLineOfFire = true;
            if (counters[i] <= 0f) {
                if (inLineOfFire && targetLayers == (targetLayers | (1 << hit.collider.gameObject.layer))) {
                    Fire(weapons[i]);
                    counters[i] = 1 / weapons[i].fireRate;
                }
            } else {
                counters[i] -= Time.deltaTime;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualLaser : ArmWeapon
{
    [SerializeField] private LineRenderer[] laserLines;

    private GameObject[] fireEffectGO;
    private GameObject[] fireImpactEffectGO;

    private void Awake()
    {
        laserLines = GetComponents<LineRenderer>();
        foreach (LineRenderer lineRenderer in laserLines) {
            lineRenderer.enabled = false;
        }
        fireEffectGO = new GameObject[laserLines.Length];
        fireImpactEffectGO = new GameObject[laserLines.Length];

        weaponDamage *= Time.deltaTime; // Scale weapon damage to be independent of frame rate
        weaponDamage *= weaponFireRate; // Scale a continiously firing weapon with fire rate
    }

    protected override void FireOn()
    {
        base.FireOn();
        for (int i = 0; i < laserLines.Length; i++) {
            laserLines[i].enabled = true;
            laserLines[i].SetPosition(0, firePoints[i].position);
        }

        FireRays();

        for (int i = 0; i < laserLines.Length; i++) {
            if (fireEffectGO[i] == null) {
                fireEffectGO[i] = Instantiate(fireEffect, firePoints[0].position, firePoints[0].rotation);
                fireEffectGO[i].transform.parent = firePoints[0];
                fireEffectGO[i].transform.localScale = fireEffect.transform.localScale;
                fireEffectGO[i].transform.localPosition = fireEffect.transform.localPosition;
            }
        }

    }

    protected override void FireOff()
    {
        base.FireOff();
        for (int i = 0; i < laserLines.Length; i++) {
            laserLines[i].enabled = false;

            if (fireEffectGO[i] != null) {
                Destroy(fireEffectGO[i]);
                fireEffectGO[i] = null;
            }
            if (fireImpactEffectGO[i] != null) {
                Destroy(fireImpactEffectGO[i]);
                fireImpactEffectGO[i] = null;
            }
        }
    }

    protected override void OnHitFireRayX(RaycastHit hit, int i)
    {
        base.OnHitFireRayX(hit, i);
        laserLines[i].SetPosition(1, hit.point);

        if (fireImpactEffectGO[i] == null) {
            fireImpactEffectGO[i] = Instantiate(fireImpactEffect, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
        } else {
            fireImpactEffectGO[i].transform.position = hit.point - 0.03f * firePoints[0].forward;
            fireImpactEffectGO[i].transform.rotation = Quaternion.LookRotation(firePoints[0].forward, Vector3.up);
        }
    }

    protected override void OnMissFireRayX(int i)
    {
        base.OnMissFireRayX(i);
        laserLines[i].SetPosition(1, firePoints[0].position + firePoints[0].forward * 200f);

        if (fireImpactEffectGO[i] != null) {
            Destroy(fireImpactEffectGO[i]);
            fireImpactEffectGO[i] = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Laser : ArmWeapon
{
    [SerializeField] private LineRenderer laserLine;

    private GameObject fireEffectGO;
    private GameObject fireImpactEffectGO;

    private void Awake()
    {
        laserLine.enabled = false;
        fireEffectGO = null;
        fireImpactEffectGO = null;
        
        weaponDamage *= Time.deltaTime; // Scale weapon damage to be independent of frame rate
        weaponDamage *= weaponFireRate; // Scale a continiously firing weapon with fire rate
    }

    protected override void FireOn()
    {
        laserLine.SetPosition(0, firePoints[0].position);
        laserLine.enabled = true;

        FireRays();

        if (fireEffectGO == null) {
            fireEffectGO = Instantiate(fireEffect, firePoints[0].position, firePoints[0].rotation);
            fireEffectGO.transform.parent = firePoints[0];
            fireEffectGO.transform.localScale = fireEffect.transform.localScale;
            fireEffectGO.transform.localPosition = fireEffect.transform.localPosition;
        }
        base.FireOn();

    }

    protected override void FireOff()
    {
        base.FireOff();
        laserLine.enabled = false;
        if (fireEffectGO != null) {
            Destroy(fireEffectGO);
            fireEffectGO = null;
        }
        if (fireImpactEffectGO != null) {
            Destroy(fireImpactEffectGO);
            fireImpactEffectGO = null;
        }
    }

    protected override void OnHitFireRay(RaycastHit hit)
    {
        base.OnHitFireRay(hit);
        laserLine.SetPosition(1, hit.point);

        if (fireImpactEffectGO == null) {
            fireImpactEffectGO = Instantiate(fireImpactEffect, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
        } else {
            fireImpactEffectGO.transform.position = hit.point - 0.03f * firePoints[0].forward;
            fireImpactEffectGO.transform.rotation = Quaternion.LookRotation(firePoints[0].forward, Vector3.up);
        }
    }

    protected override void OnMissFireRay()
    {
        base.OnMissFireRay();
        laserLine.SetPosition(1, firePoints[0].position + firePoints[0].forward * 75f);

        if (fireImpactEffectGO != null) {
            Destroy(fireImpactEffectGO);
            fireImpactEffectGO = null;
        }
    }
}

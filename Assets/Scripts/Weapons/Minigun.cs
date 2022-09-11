using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Minigun : ArmWeapon
{
    [SerializeField] private Transform barrelTransform;

    [Header("Weapon Specifics")]
    [SerializeField] private float barrelRotationSpeed = 90f;

    private float counter;
    private GameObject fireEffectGO;

    void Start()
    {
        counter = 1 / weaponFireRate;
        fireEffectGO = null;
    }

    private void RotateBarrel()
    {
        barrelTransform.Rotate(0f, 0f, barrelRotationSpeed * Time.deltaTime, Space.Self);
    }

    protected override void FireOn()
    {
        base.FireOn();

        if (counter <= 0f) {
            FireRays();
            counter = 1 / weaponFireRate;
        } else {
            counter -= Time.deltaTime;
        }
        if (fireEffectGO == null) {
            fireEffectGO = Instantiate(fireEffect, firePoints[0]);
        }

        RotateBarrel();
    }

    protected override void FireOff()
    {
        base.FireOff();
        if (fireEffectGO != null)
            Destroy(fireEffectGO);
    }

    protected override void OnHitFireRay(RaycastHit hit)
    {
        GameObject fireImpactEffectGO = Instantiate(fireImpactEffect, hit.point, Quaternion.LookRotation(-1f * hit.normal, Vector3.up));
        Destroy(fireImpactEffectGO, 0.1f);
    }
}

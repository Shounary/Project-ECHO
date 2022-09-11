using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class ArmWeapon : Weapon
{
    [SerializeField] protected WeaponClass weaponClass = WeaponClass.ArmWeapon;
    [SerializeField] protected InputActionReference fireActionReference;
    [SerializeField] protected float weaponDamage, weaponFireRate = 1f, attackRange = 100f;

    [Header("References")]
    [SerializeField] protected Transform[] firePoints;
    [SerializeField] protected LayerMask damageLayers, ignoreLayers;
    [SerializeField] protected AudioSource weaponAudio;
    [SerializeField] protected GameObject fireEffect;
    [SerializeField] protected GameObject fireImpactEffect;

    [Header("HapticFeedback")]
    [SerializeField] protected bool hapticEnabled = true;
    [SerializeField] protected XRBaseController controller;
    [SerializeField] protected float hapticAmplitude = 0.4f;

    protected virtual void Update()
    {
        //FireOn();
        //return;
        float value = fireActionReference.action.ReadValue<float>();
        //value = Input.GetAxis("Vertical");
        if (value > 0) {
            FireOn();
        } else {
            FireOff();
        }
    }

    protected virtual void FireOn()
    {
        if (!weaponAudio.isPlaying)
            weaponAudio.Play();

        if (hapticEnabled) {
            controller.SendHapticImpulse(hapticAmplitude, 0.1f);
        }
    }

    protected virtual void FireOff()
    {
        weaponAudio.Stop();
    }

    protected virtual void FireRays()
    {
        for (int i = 0; i < firePoints.Length; i++) {
            RaycastHit hit;
            if (Physics.Raycast(firePoints[i].position, firePoints[i].forward, out hit, attackRange, ~(ignoreLayers))) {
                if (damageLayers == (damageLayers | (1 << hit.collider.gameObject.layer))) {
                    hit.collider.gameObject.GetComponentInParent<Entity>().TakeDamage(weaponDamage);
                }
                OnHitFireRayX(hit, i);
                GameObject fireImpactEffectGO = Instantiate(fireImpactEffect, hit.point, Quaternion.LookRotation(-1f * hit.normal, Vector3.up));
                Destroy(fireImpactEffectGO, 0.1f);
            } else {
                OnMissFireRayX(i);
            }
        }
    }

    protected virtual void OnHitFireRayX(RaycastHit hit, int index)
    {
        if (firePoints.Length == 1) {
            OnHitFireRay(hit);
        }
    }

    protected virtual void OnMissFireRayX(int index)
    {
        if (firePoints.Length == 1) {
            OnMissFireRay();
        }
    }

    protected virtual void OnHitFireRay(RaycastHit hit)
    {

    }

    protected virtual void OnMissFireRay()
    {

    }
}

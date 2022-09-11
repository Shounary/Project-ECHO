using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : ArmWeapon
{
    [SerializeField] private ParticleSystem fireVFX;
    [SerializeField] private GameObject fireCollider;

    private void Start()
    {
        weaponDamage *= Time.deltaTime; // Scale weapon damage to be independent of frame rate
        weaponDamage *= weaponFireRate; // Scale a continiously firing weapon with fire rate
    }

    protected override void FireOn()
    {
        fireVFX.Play(true);
        fireCollider.SetActive(true);
        base.FireOn();
    }

    protected override void FireOff()
    {
        fireVFX.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        fireCollider.SetActive(false);
        base.FireOff();
    }

    private void OnTriggerStay(Collider other)
    {
        if (damageLayers == (damageLayers | (1 << other.gameObject.layer))) {
            other.gameObject.GetComponentInParent<Entity>().TakeDamage(weaponDamage);
        }
    }
}

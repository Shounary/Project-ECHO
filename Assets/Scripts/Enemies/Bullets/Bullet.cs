using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected GameObject fireImpactEffect;
    [SerializeField] protected LayerMask ignoreLayers;

    [SerializeField] protected AudioSource impactAudio;

    [HideInInspector]
    public float damage { get; set; }

    [HideInInspector]
    public LayerMask damageLayers { get; set; }

    [HideInInspector]
    public WeaponDamageType weaponDamageType { get; set; }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (ignoreLayers == (ignoreLayers | (1 << collision.gameObject.layer)))
            return;
        if (damageLayers == (damageLayers | (1 << collision.gameObject.layer))) {
            OnTargetHit(collision);
        } else {
            OnObstacleHit(collision);
        }
        PlayVFX(collision.GetContact(0).point);
        Destroy(gameObject);
    }

    protected virtual void PlayVFX(Vector3 contact)
    {
        GameObject fireImpactEffectGO = Instantiate(fireImpactEffect, transform.position, transform.rotation);
        Destroy(fireImpactEffectGO, 0.1f);
    }

    protected virtual void PlaySFX()
    {
        if (impactAudio != null) {
            AudioSource impactAudioGO = Instantiate(impactAudio, transform.position, transform.rotation);
            impactAudioGO.Play();
            Destroy(impactAudioGO, 1.4f);
        }
    }

    protected virtual void OnTargetHit(Collision collision)
    {
        collision.gameObject.GetComponentInParent<Entity>().TakeDamage(damage);
        PlaySFX();
    }

    protected virtual void OnObstacleHit(Collision collision)
    {
        PlaySFX();
    }


}

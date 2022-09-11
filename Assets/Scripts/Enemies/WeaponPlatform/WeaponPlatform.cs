using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlatform : Entity
{
    [SerializeField] protected PlatformWeapon[] weapons;
    [Range(0, 1)]
    [SerializeField] protected float aimFactor = 1f;
    [SerializeField] protected LayerMask damageLayers, ignoreLayers, targetLayers;
    [SerializeField] protected GameObject deathEffect;

    [System.Serializable]
    public class PlatformWeapon
    {
        [Header("Parameters")]
        [SerializeField] public float attackRange = 25f;
        [SerializeField] public float damage = 10f;
        [SerializeField] public float fireRate = 4f;
        [SerializeField] public float bulletSpeed = 30f;
        [SerializeField] public WeaponDamageType damageType;

        [Header("References")]
        [SerializeField] public GameObject bullet;
        [SerializeField] public AudioSource fireAudio;
        [SerializeField] public Transform[] firePoints;
        [SerializeField] public GameObject fireEffect;
    }

    protected virtual void Fire(PlatformWeapon weapon)
    {
        // Bullet SetUp
        int firePointNum = Random.Range(0, weapon.firePoints.Length);
        GameObject bulletGO = Instantiate(weapon.bullet, weapon.firePoints[firePointNum].position, weapon.firePoints[firePointNum].rotation);

        Vector3 aimOffset = weapon.firePoints[firePointNum].right * Random.Range(-1 + aimFactor, 1 - aimFactor)
            + weapon.firePoints[firePointNum].up * Random.Range(-1 + aimFactor, 1 - aimFactor);
        bulletGO.GetComponent<Rigidbody>().velocity = weapon.firePoints[firePointNum].forward * weapon.bulletSpeed + aimOffset;

        Bullet bulletParameters = bulletGO.GetComponent<Bullet>();
        bulletParameters.damage = weapon.damage;
        bulletParameters.damageLayers = damageLayers;
        bulletParameters.weaponDamageType = weapon.damageType;

        Destroy(bulletGO, weapon.attackRange / weapon.bulletSpeed + 0.1f);


        // VFX SetUp
        GameObject fireEffectGO = Instantiate(weapon.fireEffect, weapon.firePoints[firePointNum]);
        Destroy(fireEffectGO, 0.7f / weapon.fireRate);

        // SFX SetUP
        if (!weapon.fireAudio.isPlaying)
            weapon.fireAudio.Play();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (health <= 0f) {
            GameObject deathEffectGO = Instantiate(deathEffect, transform.position, transform.rotation);
            // TODO: add death animation and play it
            Destroy(deathEffectGO, 1.5f);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : Entity
{
    [Header("Parameters")]
    [SerializeField] protected float enemyAttackRange = 25f;
    [SerializeField] protected float enemyDamage = 10f;
    [SerializeField] protected float enemyFireRate = 4f;
    [SerializeField] protected float enemyBulletSpeed = 30f;
    [SerializeField] protected WeaponDamageType weaponDamageType;
    [SerializeField] protected LayerMask damageLayers, ignoreLayers, targetLayers;
    [Range(0, 1)]
    [SerializeField] protected float aimFactor = 1f;
    [Header("References")]
    [SerializeField] protected GameObject enemyBullet;
    [SerializeField] protected AudioSource enemyFireAudio;
    [SerializeField] protected Transform[] firePoints;
    [SerializeField] protected GameObject fireEffect;
    [SerializeField] protected GameObject deathEffect;

    protected virtual void Fire()
    {
        // Bullet SetUp
        int firePointNum = Random.Range(0, firePoints.Length);
        GameObject bulletGO = Instantiate(enemyBullet, firePoints[firePointNum].position, firePoints[firePointNum].rotation);

        Vector3 aimOffset = firePoints[firePointNum].right * Random.Range(-1 + aimFactor, 1 - aimFactor) 
            + firePoints[firePointNum].up * Random.Range(-1 + aimFactor, 1 - aimFactor);
        bulletGO.GetComponent<Rigidbody>().velocity = firePoints[firePointNum].forward * enemyBulletSpeed + aimOffset;

        Bullet bulletParameters = bulletGO.GetComponent<Bullet>();
        bulletParameters.damage = enemyDamage;
        bulletParameters.damageLayers = damageLayers;
        bulletParameters.weaponDamageType = weaponDamageType;

        Destroy(bulletGO, enemyAttackRange / enemyBulletSpeed + 0.1f);


        // VFX SetUp
        GameObject fireEffectGO = Instantiate(fireEffect, firePoints[firePointNum]);
        Destroy(fireEffectGO, 0.7f / enemyFireRate);

        // SFX SetUP
        if (!enemyFireAudio.isPlaying)
            enemyFireAudio.Play();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (health <= 0f) {
            GameObject deathEffectGO = Instantiate(deathEffect, transform.position, transform.rotation);
            // TODO: add death animation and play it
            Destroy(deathEffectGO, 1f);
            Destroy(gameObject);
        }
    }
}

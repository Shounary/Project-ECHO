using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet
{
    [SerializeField] protected float explosionRadius = 1.2f;

    protected void Explode(Vector3 origin)
    {
        Collider[] hits = Physics.OverlapSphere(origin, explosionRadius, damageLayers);
        foreach (Collider hit in hits) {
            Entity entity = hit.gameObject.GetComponent<Entity>();
            if (entity != null) {
                bool isPathClear = Physics.Raycast(transform.position, transform.position - hit.gameObject.transform.position, explosionRadius, ~(ignoreLayers));
                if (isPathClear) {
                    entity.TakeDamage(damage);
                }
            }
        }
    }

    protected override void PlayVFX(Vector3 contact)
    {
        GameObject fireImpactEffectGO = Instantiate(fireImpactEffect, transform.position, transform.rotation);
        Destroy(fireImpactEffectGO, 1.2f);
    }

    protected override void OnObstacleHit(Collision collision)
    {
        base.OnObstacleHit(collision);
        Explode(transform.position);
    }
}

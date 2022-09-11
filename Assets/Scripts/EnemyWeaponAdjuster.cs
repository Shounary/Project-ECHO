using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponAdjuster : MonoBehaviour
{
    [SerializeField] private Transform aimHandTransform;
    [SerializeField] private Vector3 aimHandOffset;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(aimHandTransform.position - transform.position, Vector3.up);
    }
}

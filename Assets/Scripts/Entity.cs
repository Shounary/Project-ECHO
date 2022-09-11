using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float health = 1f;

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }
}

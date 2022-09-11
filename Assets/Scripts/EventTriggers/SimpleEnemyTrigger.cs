using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyTrigger : PlayerTrigger
{
    [SerializeField] private SimpleEnemy[] enemies;

    protected override void TriggerEvent()
    {
        base.TriggerEvent();
        foreach (SimpleEnemy enemy in enemies) {
            enemy.enabled = true;
        }
    }
}

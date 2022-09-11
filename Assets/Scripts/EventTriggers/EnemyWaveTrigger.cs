using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveTrigger : PlayerTrigger
{
    [SerializeField] private GameObject enemyAIZone;

    protected override void TriggerEvent()
    {
        base.TriggerEvent();

        enemyAIZone.SetActive(true);

        CoverZoneAI aI = enemyAIZone.GetComponent<CoverZoneAI>();
        //if (aI != null) {
        //    aI.Initiate();
        //}
    }
}

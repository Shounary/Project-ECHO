using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TanatosActivationTrigger : PlayerTrigger
{
    [SerializeField] private GameObject ship;
    [SerializeField] private GameObject cinematicZone;
    [SerializeField] private GameObject combatZone;

    [SerializeField] private float cinematicTime = 7f;

    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = cinematicTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && counter <= 0) {
            cinematicZone.SetActive(false);
            combatZone.SetActive(true);
            this.enabled = false;
        } else {
            counter -= Time.deltaTime;
        }
    }

    protected override void TriggerEvent()
    {
        base.TriggerEvent();
        ship.SetActive(true);
        cinematicZone.SetActive(true);
    }

    public override void Execute()
    {
        base.Execute();
        counter = cinematicTime;
        isActive = true;
    }
}

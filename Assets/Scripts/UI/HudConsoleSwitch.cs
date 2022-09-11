using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudConsoleSwitch : UIDelayTrigger
{
    [SerializeField] private GameObject[] hud;
    [SerializeField] private GameObject[] toDisable;
    [SerializeField] private GameObject[] toEnable;
    [SerializeField] private GameObject[] executables;

    public void ToggleSwitch()
    {
        foreach (GameObject gameObject in toDisable) {
            if (gameObject != null)
                gameObject.SetActive(false);
        }
        foreach (GameObject gameObject in toEnable) {
            if (gameObject != null)
                gameObject.SetActive(true);
        }
    }

    protected override void TriggerActivateEvent()
    {
        base.TriggerActivateEvent();
        ToggleSwitch();

        foreach(GameObject e in executables) {
            e.GetComponent<IExecutable>().Execute();
        }

        DeactivateHUD();
    }

    protected override void TriggerExitEvent()
    {
        base.TriggerExitEvent();
        DeactivateHUD();
    }

    protected override void TriggerEnterEvent()
    {
        base.TriggerEnterEvent();
        ActivateHUD();
    }



    private void ActivateHUD()
    {
        foreach (GameObject hudItem in hud) {
            hudItem.SetActive(true);
        }
    }

    private void DeactivateHUD()
    {
        foreach (GameObject hudItem in hud) {
            hudItem.SetActive(false);
        }
    }
}

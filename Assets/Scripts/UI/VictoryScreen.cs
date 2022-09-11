using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private GameObject victoryCanvas;
    [SerializeField] private GameObject[] toDisable;
    [SerializeField] private TMPro.TMP_Text tmp;


    private void OnDestroy()
    {
        foreach (GameObject go in toDisable) {
            go.SetActive(false);
        }
        victoryCanvas.SetActive(true);
        tmp.text = "Run Time: " + System.Math.Round(Time.timeSinceLevelLoad, 2) + " s";

    }
}

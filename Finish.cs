using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteCanvas;
    public void FinishLevel()
    {
        levelCompleteCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}

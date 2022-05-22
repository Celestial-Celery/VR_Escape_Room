using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;

    public void OpenPause()
    {
        pauseCanvas.SetActive(true);
    }

    public void ClosePause()
    {
        pauseCanvas.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        //return to main menu
    }
}

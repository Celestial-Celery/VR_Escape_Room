using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    private GameObject XRRig;

    private void Start()
    {
        XRRig = GameObject.Find("XR Rig");
    }

    private void Update()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        transform.LookAt(XRRig.transform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z);
    }

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
        transform.GetComponent<TeleportToMenu>().Teleport();
        ClosePause();
    }
}

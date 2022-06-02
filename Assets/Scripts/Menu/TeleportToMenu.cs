using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportToMenu : MonoBehaviour
{
    [SerializeField] MenuManager menuManager;
    [SerializeField] Transform menuSpawnPoint;
    [SerializeField] GameObject XRRig;

    public void Teleport()
    {
        menuManager.GameActive = false;
        menuManager.MenuRoom.SetActive(true);
        menuManager.ShowGameCompleted();
        XRRig.transform.position = menuSpawnPoint.position;
        menuManager.EscapeRoom.SetActive(false);
    }
}

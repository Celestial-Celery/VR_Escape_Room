using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportToMenu : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] MenuManager menuManager;
    [SerializeField] Transform menuSpawnPoint;
    [SerializeField] GameObject XRRig;

    public void Teleport()
    {
        menuManager.MenuRoom.SetActive(true);
        menuManager.ShowGameCompleted();
        if (GameObject.Find("Game_Manager").GetComponent<GameManager>().EscapeRoomState == GameManagerState.Ended)
        {
            //teleportRequest.destinationPosition = menuSpawnPoint.position;
            //teleportRequest.destinationRotation = menuSpawnPoint.rotation;

            XRRig.transform.position = menuSpawnPoint.position;
        }
        menuManager.EscapeRoom.SetActive(false);
    }
}

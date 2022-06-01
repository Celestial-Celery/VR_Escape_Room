using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToMenu : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Transform menuSpawnPoint;
    [SerializeField] GameObject XRRig;

    public void Teleport()
    {
        if (gameManager.EscapeRoomState == GameManagerState.Ended)
        {
            XRRig.transform.position = menuSpawnPoint.position;
        }
    }
}

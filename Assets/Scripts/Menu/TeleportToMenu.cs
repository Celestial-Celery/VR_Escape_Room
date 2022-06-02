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
        menuManager.MenuRoom.SetActive(true);
        menuManager.ShowGameCompleted();
        XRRig.transform.position = menuSpawnPoint.position;
        //if (GameObject.Find("Game_Manager").GetComponent<GameManager>().EscapeRoomState == GameManagerState.Ended)
        //{
            
        //}
        menuManager.EscapeRoom.SetActive(false);
    }
}

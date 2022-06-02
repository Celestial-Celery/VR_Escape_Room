using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPadScaler : MonoBehaviour
{
    [SerializeField] List<GameObject> Teleportpads;
    [SerializeField] float handsScale = 0.3f;
    [SerializeField] float defaultScale = 0.15f;

    void Update()
    {
        foreach (GameObject pad in Teleportpads)
        {
            if (HandManager.handsActive)
            {
                pad.transform.localScale = new Vector3(handsScale, handsScale, handsScale);
            }
            else
            {
                pad.transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);
            } 
        }
    }
}

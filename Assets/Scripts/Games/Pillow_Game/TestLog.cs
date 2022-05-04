using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLog : MonoBehaviour
{
    public void SendPointing()
    {
        Debug.Log("Gesture: Pointing!");
    }

    public void SendNotPointing()
    {
        Debug.Log("Gesture: No longer Pointing!");
    }
}

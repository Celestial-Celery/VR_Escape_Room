using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameObject XRRig;
    private bool isPlayerSitting;

    //Loads all settings at startup, is called by MenuManager
    public void LoadSettings()
    {
        isPlayerSitting = PlayerPrefs.GetInt("sitting") == 1;
        SetUserHeight();
    }

    //Sets the users height depending on wether they selected sitting only or free movement as setting.
    public void SetUserHeight()
    {
        if (isPlayerSitting)
        {
            XRRig.transform.localPosition = new Vector3(XRRig.transform.localPosition.x, 0.30f, XRRig.transform.localPosition.z);
        }
        else
        {
            XRRig.transform.localPosition = new Vector3(XRRig.transform.localPosition.x, 0, XRRig.transform.localPosition.z);
        }
    }

    private void Update()
    {
        SetUserHeight();
    }
}

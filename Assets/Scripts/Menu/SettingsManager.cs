using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameObject XRRig;

    //Loads all settings at startup, is called by MenuManager
    public void LoadSettings()
    {
        SetUserHeight();
    }

    //Sets the users height depending on wether they selected sitting only or free movement as setting.
    public void SetUserHeight()
    {
        if (PlayerPrefs.GetInt("sitting") == 1)
        {
            XRRig.transform.localPosition = new Vector3(0,0.40f,0);
        }
        else
        {
            XRRig.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}

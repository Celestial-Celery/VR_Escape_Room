using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool _found = false;

    void Update()
    {
        if (_found)
        {

        }
    }
    public void Found()
    {
        this._found = true;
    }
}

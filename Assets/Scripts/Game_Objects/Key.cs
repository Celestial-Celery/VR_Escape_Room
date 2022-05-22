using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public FinalDoor Door;

    public int Number = 0;

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

    public void PickUp()
    {
        switch (Number)
        {
            case 1:
                this.Door.AddKey1(this.gameObject);
                break;
            case 2:
                this.Door.AddKey2(this.gameObject);
                break;
            case 3:
                this.Door.AddKey3(this.gameObject);
                break;
            default:
                break;
        }
    }
}

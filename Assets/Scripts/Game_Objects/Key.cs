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
        if (HandManager.handsActive)
        {
            if(this.Number == 3) //yikes code but idk how else
            {
                if (this._found)
                {
                    Debug.Log("Hands active and key 3 found");
                    this.PickUp();
                }
            }
            else
            {
                this.PickUp();
            }
            
        }
    }
    public void Found()
    {
        this._found = true;
    }

    public void PickUp()
    {
        switch (this.Number)
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

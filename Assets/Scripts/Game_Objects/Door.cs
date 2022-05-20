using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DoorState {Open,Closed}

public class Door : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 60;
    private DoorState doorState = DoorState.Closed;
    private bool openDoor = false;
    private bool closeDoor = false;

    public void Open()
    {
        if (doorState == DoorState.Closed)
        {
            openDoor = true;
            doorState = DoorState.Open;
        }
    }
    public void Close()
    {
        if (doorState == DoorState.Open)
        {
            closeDoor = true;
            doorState = DoorState.Closed;
        }
    }

    private void Update()
    {
        if (openDoor)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + (rotationSpeed * Time.deltaTime));
            if (transform.localEulerAngles.y >= 220)
            {
                openDoor = false;
            }
        }

        if (closeDoor)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - (rotationSpeed * Time.deltaTime));
            if (transform.localEulerAngles.y <= 90)
            {
                closeDoor = false;
            }
        }
    }
}

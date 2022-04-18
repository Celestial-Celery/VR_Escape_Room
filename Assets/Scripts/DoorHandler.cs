using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move X axis with low speed by 80 units of Door object
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 80, 0), 0.009f);

        // check if Door object is rotated to 80 degrees
        // and if it is, then destroy Door object
        if (transform.rotation.eulerAngles.y == 80)
        {
            Debug.Log("Door is open");
        }

    }

    /* Important notes
    To change scene use: UnityEngine.SceneManagement.SceneManager.LoadScene("GameManager");
    Useful resources: https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.html
    */
}

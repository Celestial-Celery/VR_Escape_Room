using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move camera with low speed to the front
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(0.067f, 2f, 2), 0.005f);
        
    }


}

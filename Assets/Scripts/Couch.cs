using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couch : MonoBehaviour
{
    public List<Pillow> pillows;
    private int keyPos;

    void Start()
    {
        keyPos = Random.Range(0, pillows.Count);
        pillows[keyPos].hasKey = true;
        Spawnkey(pillows[keyPos]);
    }

    void Update()
    {
        Pillow removedPillow = null;

        foreach(Pillow pillow in pillows)
        {
            if (pillow.isSelected)
            {
                if (pillow.hasKey)
                {
                    Debug.Log("Key has been found!");
                    //TODO key found logic here
                }
                //Maybe something more elegant than just poof gone
                removedPillow = pillow;
                Destroy(pillow.gameObject);
            }
        }  
        
        if (removedPillow)
        {
            pillows.Remove(removedPillow);
        }
    }

    private void Spawnkey(Pillow pillow)
    {
        GameObject key = GameObject.Find("Key_Couch");
        key.transform.position = new Vector3(pillow.transform.position.x, pillow.transform.position.y, pillow.transform.position.z);
    }
}

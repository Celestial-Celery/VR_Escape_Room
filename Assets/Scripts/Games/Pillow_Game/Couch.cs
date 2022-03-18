using System.Collections.Generic;
using UnityEngine;

public class Couch : MonoBehaviour
{
    public List<Pillow> Pillows;
    private int _keyPos;

    //Called on start
    void Start()
    {
        _keyPos = Random.Range(0, Pillows.Count);
        Pillows[_keyPos].HasKey = true;
        SpawnKey(Pillows[_keyPos]);
    }

    //Called by pillow when it is selected
    public static void Select(Pillow pillow)
    {
        if (pillow.HasKey)
        {
            KeyFound();
        }
        //Maybe something more elegant than just poof gone
        Destroy(pillow.gameObject);
    }

    //Puts key under the pillow that has the key
    private void SpawnKey(Pillow pillow)
    {
        GameObject key = GameObject.Find("Key_Couch");
        key.transform.position = new Vector3(pillow.transform.position.x, pillow.transform.position.y, pillow.transform.position.z);
    }

    //Key found logic would be here
    private static void KeyFound()
    {
        Debug.Log("Key has been found!");
    }
}

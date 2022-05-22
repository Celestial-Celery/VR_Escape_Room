using System.Collections.Generic;
using UnityEngine;

public class Couch : Game
{
    public List<Pillow> Pillows;
    private int _keyPos;
    private static bool _keyfound = false;

    //Called on start
    void Start()
    {
        _keyPos = Random.Range(0, Pillows.Count);
        Pillows[_keyPos].HasKey = true;
        SpawnKey(Pillows[_keyPos]);
        UnparentPillows();

        this.GameState = GameState.InProgress;
    }

    //Called by pillow when it is selected
    public static void Select(Pillow pillow, Key _key, Couch couch)
    {
        if (pillow.HasKey && !_keyfound)
        {
            _keyfound = true;
            KeyFound(_key, couch);
        }
        //Maybe something more elegant than just poof gone
        //Destroy(pillow.gameObject);
    }

    //Puts key under the pillow that has the key
    private void SpawnKey(Pillow pillow)
    {
        GameObject key = GameObject.Find("Key_3");
        Transform keypos = pillow.transform.GetChild(0).transform;
        key.transform.position = keypos.position;
    }

    private void UnparentPillows()
    {
        foreach(Pillow pillow in Pillows)
        {
            //pillow.transform.SetParent(null);
            pillow.transform.parent = this.gameObject.transform;
        }
    }

    //Key found logic would be here
    private static void KeyFound(Key _key, Couch couch)
    {
        _key.Found();
        Debug.Log("Key has been found!");
        couch.GameState = GameState.InProgress;
    }
}

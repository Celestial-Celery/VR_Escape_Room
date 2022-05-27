using System.Collections.Generic;
using UnityEngine;

public class Couch : Game
{
    public List<Pillow> Pillows;
    private int _keyPos;
    private static bool _keyfound = false;

    private Key keyInGame;

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
    public void Select(Pillow pillow)
    {
        if (pillow.HasKey && !_keyfound)
        {
            _keyfound = true;
            this.KeyFound();
        }
        //Maybe something more elegant than just poof gone
        //Destroy(pillow.gameObject);
    }

    //Puts key under the pillow that has the key
    private void SpawnKey(Pillow pillow)
    {
        keyInGame = Instantiate(this.Key, this.transform.position, this.transform.rotation);
        keyInGame.Door = this.Door;
        Transform keypos = pillow.transform.GetChild(0).transform;
        keyInGame.transform.position = keypos.position;
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
    private void KeyFound()
    {
        this.keyInGame.Found();
        Debug.Log("Key has been found!");
        this.GameState = GameState.Completed;
    }
}

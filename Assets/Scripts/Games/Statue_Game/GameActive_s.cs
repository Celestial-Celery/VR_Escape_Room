using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameActivate_s : MonoBehaviour
{
    public Statue_Game statue_game;

    public void Activate_Game()
    {
        this.statue_game.activeGame = true;
    }
}

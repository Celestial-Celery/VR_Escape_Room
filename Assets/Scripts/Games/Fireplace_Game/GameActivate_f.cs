using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameActivate_f : MonoBehaviour
{
    public Flame_Game flame_game;

    public void Activate_Game()
    {
        this.flame_game.activeGame = true;
    }
}

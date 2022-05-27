using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    InProgress,
    Completed,
    CompletionRegistered
}
public class Game : MonoBehaviour
{
    public FinalDoor Door;
    public Key Key;

    public GameState GameState;

    private void Start()
    {
        this.GameState = GameState.InProgress;
    }
}
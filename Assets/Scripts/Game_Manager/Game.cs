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
    public GameState GameState;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameManagerState
{
    Start,
    InProgress,
    Paused,
    Completed,
    Ended
}

public class GameManager : MonoBehaviour
{
    public Game[] Games;

    private bool _escapeRoomStarted;
    public GameManagerState EscapeRoomState;
    private int _gamesCount;
    private int _gamesCompleted;

    private void Start()
    {
        this.ResetEscapeRoom();
    }

    private void ResetEscapeRoom()
    {
        this._escapeRoomStarted = false;
        this.EscapeRoomState = GameManagerState.Start;
        this._gamesCount = Games.Length;
        this._gamesCompleted = 0;
    }

    private void FixedUpdate()
    {
        if (!this._escapeRoomStarted)
        {
            this._escapeRoomStarted= true;
            StartCoroutine(EscapeRoomGameLogic());
        }
    }

    private IEnumerator EscapeRoomGameLogic()
    {
        while (this._escapeRoomStarted)
        {
            while(this.EscapeRoomState == GameManagerState.Start)
            {
                //Code that runs at the start of the escape room
                this.StartEscapeRoom();
                yield return null;
            }

            while(this.EscapeRoomState == GameManagerState.InProgress)
            {
                //here code for when you are playing the escape room
                this.CheckGameStates();
                this.CheckEscapeRoomFinished();
                yield return null;
            }

            while(this.EscapeRoomState == GameManagerState.Completed)
            {
                //code to run when the escape room is completed
                this.EndEscapeRoom();
                yield return null;
            }

            while(this.EscapeRoomState == GameManagerState.Ended)
            {
                yield return null;
            }
        }
    }

    private void StartEscapeRoom()
    {
        //Here code to start the game
        Debug.Log("Welcome to the escape room!");
        this.EscapeRoomState = GameManagerState.InProgress;
    }

    private void CheckGameStates()
    {
        foreach (Game game in Games)
        {
            if (game.GameState == GameState.Completed)
            {
                Debug.Log("You have completed a game!");
                this._gamesCompleted++;
                game.GameState = GameState.CompletionRegistered;
            }
        }
    }

    private void CheckEscapeRoomFinished()
    {
        if (this._gamesCompleted == this._gamesCount)
        {
            this.EscapeRoomState = GameManagerState.Completed;
        }
    }

    private void EndEscapeRoom()
    {
        Debug.Log("You have completed the Escape room!");
        //here code that executes when the escape room ends
        this.EscapeRoomState = GameManagerState.Ended;
    }
}
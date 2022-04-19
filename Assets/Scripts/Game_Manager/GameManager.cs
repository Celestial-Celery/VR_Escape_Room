using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameManagerState
{
    Start,
    InProgress,
    Paused,
    Completed
}

public class GameManager : MonoBehaviour
{
    public Game[] Games;

    private bool _escapeRoomStarted;
    private GameManagerState _escapeRoomState;
    private int _gamesCount;
    private int _gamesCompleted;

    private void Start()
    {
        this._escapeRoomStarted = false;
        this._escapeRoomState = GameManagerState.Start;
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
            while(this._escapeRoomState == GameManagerState.Start)
            {
                //Code that runs at the start of the escape room
                this.StartEscapeRoom();
                yield return null;
            }

            while (this._escapeRoomState == GameManagerState.InProgress)
            {
                //here code for when you are playing the escape room
                this.CheckGameStates();
                this.CheckEscapeRoomFinished();
                yield return null;
            }

            while (this._escapeRoomState == GameManagerState.Completed)
            {
                //code to run when the escape room is completed
                this.EndEscapeRoom();
                yield return null;
            }

        }
    }

    private void StartEscapeRoom()
    {
        //Here code to start the game
        Debug.Log("Welcome to the escape room!");
        this._escapeRoomState = GameManagerState.InProgress;
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
            this._escapeRoomState = GameManagerState.Completed;
        }
    }

    private void EndEscapeRoom()
    {
        Debug.Log("You have completed the Escape room!");
        //here code that executes when the escape room ends
    }
}
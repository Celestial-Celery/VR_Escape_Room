using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonGameState
{
    EnteringCode,
    ClearingNumpad,
    CheckingCode,
    Completed
}

public class Button_Game : MonoBehaviour
{
    #region Private properties
    private int _nr1;
    private int _nr2;
    private int _nr3;

    private bool _gameStarted;
    private bool _gameCompleted;

    private ButtonGameState _buttonGameState;

    private List<Numpad_Button> _pressedButtons;
    #endregion

    #region Public properties
    public GameObject MirrorRoom;
    public Numpad_Button[] Buttons;
    public GameObject[] Hints;

    public GameObject Hint1PlaceHolder;
    public GameObject Hint2PlaceHolder;
    public GameObject Hint3PlaceHolder;
    #endregion

    #region Private methods
    private void Start()
    {
        this.GenerateCode();
        this.SpawnCodeHint();

        this._gameStarted = false;
        this._gameCompleted = false;

        this._buttonGameState = ButtonGameState.EnteringCode;

        this._pressedButtons = new List<Numpad_Button>();

        Debug.Log($"code: {this._nr1}{this._nr2}{this._nr3}");
    }

    private void GenerateCode()
    {
        this._nr1 = Random.Range(0, 3);

        do
        {
            this._nr2 = Random.Range(0, 3);
        } while(this._nr2 == this._nr1);

        do
        {
            this._nr3 = Random.Range(0, 3);
        } while(this._nr3 == this._nr1 || this._nr3 == this._nr2);
    }

    private void SpawnCodeHint()
    {
        GameObject hint1 = Instantiate(Hints[this._nr1], this.Hint1PlaceHolder.transform);
        hint1.transform.parent = this.MirrorRoom.gameObject.transform;

        GameObject hint2 = Instantiate(Hints[this._nr2], this.Hint2PlaceHolder.transform);
        hint2.transform.parent = this.MirrorRoom.gameObject.transform;

        GameObject hint3 = Instantiate(Hints[this._nr3], this.Hint3PlaceHolder.transform);
        hint3.transform.parent = this.MirrorRoom.gameObject.transform;

        this.Hint1PlaceHolder.SetActive(false);
        this.Hint2PlaceHolder.SetActive(false);
        this.Hint3PlaceHolder.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!this._gameStarted)
        {
            this._gameStarted = true;
            StartCoroutine(ButtonGameLogic());
        }
    }

    private IEnumerator ButtonGameLogic()
    {
        while (this._gameStarted && !this._gameCompleted)
        {
            while (this._buttonGameState == ButtonGameState.EnteringCode)
            {
                this.CheckNumpad(); //Checks which buttons have been pressed
                //this.CheckCode(); //should change state to either clearing numpad or checking code
                yield return null;
            }

            while (this._buttonGameState == ButtonGameState.ClearingNumpad)
            {
                Debug.Log("Clearing numpad...");
                yield return new WaitForSeconds(3);
                this.ClearNumpad(); //should change gamestate to entering code
            }

            while (this._buttonGameState == ButtonGameState.CheckingCode)
            {
                this.CheckCode(); //should change gamestate to either clear numpad or completed
                yield return new WaitForSeconds(3);
            }

            while (this._buttonGameState == ButtonGameState.Completed)
            {
                this.ClearNumpad();
                this.ButtonGameCompleted();
                yield break;
            }
        }
        
    }

    private void CheckNumpad()
    {
        foreach (Numpad_Button button in Buttons)
        {
            if(button.Selected == true)
            {
                this.PressButton(button);
            }
        }
    }

    private void ClearNumpad()
    {
        Debug.Log("Numpad cleared.");
        foreach (Numpad_Button button in Buttons)
        {
            button.IsGameSelected = false;
            button.Selected = false;
        }

        this._pressedButtons.Clear();
        this._buttonGameState = ButtonGameState.EnteringCode;
    }

    private void CheckCode()
    {
        Debug.Log($"Checking code...");
        if(this._pressedButtons.Count == 3)
        {
            if ((int)this._pressedButtons[0].NumpadValue == this._nr1 &&
            (int)this._pressedButtons[1].NumpadValue == this._nr2 &&
            (int)this._pressedButtons[2].NumpadValue == this._nr3)
            {
                Debug.Log("Code is correct!");
                this._buttonGameState = ButtonGameState.Completed;
            }
            else
            {
                Debug.Log("Code is wrong!");
                this._buttonGameState = ButtonGameState.ClearingNumpad;
            }
        }
        else
        {
            Debug.Log("Not enough input!");
            this._buttonGameState = ButtonGameState.ClearingNumpad;
        }
    }

    private void ButtonGameCompleted()
    {
        Debug.Log("Button game completed!");
        //this._gameCompleted = true;
        //Send game completed to game manager
    }
    #endregion

    #region Public methods
    public void PressButton(Numpad_Button button)
    {
        if (button.NumpadValue == NumpadValue.reset)
        {
            //Reset had been pressed
            button.Press();
            this._buttonGameState = ButtonGameState.ClearingNumpad;
        }
        else if (button.NumpadValue == NumpadValue.enter)
        {
            //Code has been entered
            button.Press();
            this._buttonGameState = ButtonGameState.CheckingCode;
        }
        else
        {
            if (!this._pressedButtons.Contains(button))
            {
                if (this._pressedButtons.Count < 3)
                {
                    this._pressedButtons.Add(button);
                    button.Press();
                }
                else //needed?
                {
                    button.Selected = false;
                }
            }
        }
    }
    #endregion
}

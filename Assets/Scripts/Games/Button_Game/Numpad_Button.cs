using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NumpadValue
{
    zero,
    one,
    two,
    three,
    four,
    five,
    six,
    seven,
    eight,
    nine,
    reset,
    enter
}

public class Numpad_Button : MonoBehaviour
{
    #region Private properties
    private Vector3 _unpressedPosition;
    private Vector3 _pressedPosition;
    private Vector3 _destination;
    private float _translationSpeed;
    #endregion

    #region Public properties
    public NumpadValue NumpadValue;
    public bool IsGameSelected;
    public bool Selected;
    #endregion

    #region Private methods
    private void Start()
    {
        this._unpressedPosition = this.transform.localPosition;
        this._pressedPosition = this.transform.localPosition + new Vector3(0,0,0.1f); // - some arbitrary number
        this._translationSpeed = 1f;

        this.IsGameSelected = false;
        this.Selected = false;
    }

    private void FixedUpdate()
    {
        if (this.IsGameSelected)
        {
            this._destination = this._pressedPosition;
        }
        else
        {
            this._destination = this._unpressedPosition;
        }

        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, this._destination, Time.deltaTime * this._translationSpeed);
    }
    #endregion

    #region Public methods
    public void Select()
    {
        this.Selected = true;
    }

    public void Press()
    {
        this.IsGameSelected = !this.IsGameSelected;
    }
    #endregion
}

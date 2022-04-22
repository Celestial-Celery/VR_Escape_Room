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
    private Color _buttonColor;
    private Color _buttonUnpressedColor;
    private Color _buttonPressedColor;
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
        this._pressedPosition = this.transform.localPosition + new Vector3(0,0,0.1f);
        this._translationSpeed = 1f;

        this.IsGameSelected = false;
        this.Selected = false;

        this._buttonUnpressedColor = Color.white;
        this._buttonPressedColor = new Color(0.7725f, 0.5373f, 0.4314f);
    }

    private void FixedUpdate()
    {
        if (this.IsGameSelected)
        {
            this._destination = this._pressedPosition;
            this._buttonColor = this._buttonPressedColor;
        }
        else
        {
            this._destination = this._unpressedPosition;
            this._buttonColor = this._buttonUnpressedColor;
        }

        this.gameObject.GetComponent<Renderer>().material.color = this._buttonColor;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayingCardColor
{
    Red, Black
}

public enum PlayingCardSuit
{
    Clubs, Diamonds, Hearts, Spades
}

public class PlayingCard : MonoBehaviour
{
    #region Private methods
    private Quaternion _faceUp;
    private Quaternion _faceDown;
    private Quaternion _destination;
    private float _rotationSpeed;
    #endregion
    #region Public properties
    public int Number;
    public PlayingCardSuit Suit;
    public PlayingCardColor Color;
    public bool Selected;
    public bool IsGameSelected;
    #endregion

    #region Private methods
    private void Start()
    {
        this._faceDown = this.transform.localRotation * Quaternion.Euler(0,180,0);
        this._faceUp = new Quaternion(0, 0, 1f, 0) * Quaternion.Euler(0, 180, 0);
        this._rotationSpeed = 180f;

        this.IsGameSelected = false;
        this.Selected = false;

        this.transform.rotation = _faceUp;
    }

    private void FixedUpdate()
    {
        if (this.IsGameSelected)
        {
            this._destination = _faceDown;
        }
        else
        {
            this._destination = _faceUp;
        }
        
        this.transform.localRotation = Quaternion.RotateTowards(this.transform.localRotation, this._destination, Time.deltaTime * this._rotationSpeed);
    }
    #endregion

    #region Public methods
    public void Select()
    {
        this.Selected = true;
    }

    public void Flip()
    {
        this.IsGameSelected = !this.IsGameSelected;
    }
    #endregion
}
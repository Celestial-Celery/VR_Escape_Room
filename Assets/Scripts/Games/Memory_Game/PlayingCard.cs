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
    private bool _isFlipping;
    #endregion
    #region Public properties
    public int Number;
    public PlayingCardSuit Suit;
    public PlayingCardColor Color;
    public bool Selected;
    #endregion

    #region Private methods
    private void Start()
    {
        this._faceDown = this.transform.localRotation;
        this._faceUp = new Quaternion(0, 0, 1f, 0);
        this._rotationSpeed = 180f;
        this._isFlipping = false;

        this.Selected = false;

        this.transform.rotation = _faceUp;
    }

    private void FixedUpdate()
    {
        if (this.Selected)
        {
            this._destination = _faceDown;
        }
        else
        {
            this._destination = _faceUp;
        }
        
        this.transform.localRotation = Quaternion.RotateTowards(this.transform.localRotation, this._destination, Time.deltaTime * this._rotationSpeed);


        //if (this._isFlipping)
        //{
            
        //    if (this.transform.rotation == this._faceDown)
        //    {
        //        Debug.Log("Stopped flipping");
        //        this._isFlipping = false;
        //    }
        //}
    }
    #endregion

    #region Public methods
    #endregion
}
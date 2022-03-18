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
    #region Public properties
    public int Number;
    public PlayingCardSuit Suit;
    public PlayingCardColor Color;
    public bool Selected;
    #endregion

    #region Private Methods
    private void Start()
    {
        this.Selected = false;
    }
    #endregion
}
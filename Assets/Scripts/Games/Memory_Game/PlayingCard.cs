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

    public bool Selected = false;
    #endregion

    #region Private Methods
    private void Update()
    {
        if (this.Selected)
        {
            this.Select();
        }
    }
    #endregion

    #region Public Methods
    public void Select()
    {
        MemoryGame.SelectCard(this);
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingCardDeck : MonoBehaviour
{
    #region Public Properties
    public List<PlayingCard> Clubs;
    public List<PlayingCard> Diamonds;
    public List<PlayingCard> Hearts;
    public List<PlayingCard> Spades;
    #endregion

    #region Public Methods
    public PlayingCard GetPlayingCard(int number, PlayingCardSuit suit)
    {
        PlayingCard playingCard = null;

        switch (suit)
        {
            case PlayingCardSuit.Clubs:
                playingCard = this.Clubs[number - 1];
                break;
            case PlayingCardSuit.Diamonds:
                playingCard = this.Diamonds[number - 1];
                break;
            case PlayingCardSuit.Hearts:
                playingCard = this.Hearts[number - 1];
                break;
            case PlayingCardSuit.Spades:
                playingCard = this.Spades[number - 1];
                break;
            default:
                break;
        }

        return playingCard;
    }
    #endregion
}
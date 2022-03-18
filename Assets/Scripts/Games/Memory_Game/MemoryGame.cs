using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGame : MonoBehaviour
{
    #region Private Properties
    private List<PlayingCard> _cardsInGame;
    private static List<PlayingCard> _selectedCards;
    #endregion

    #region Public Properties
    public PlayingCardDeck Deck;
    public int PairAmount;
    #endregion

    #region Private Methods
    private void Start()
    {
        this.GeneratePairs(PairAmount);
        this.SpawnCards();
    }

    private void GeneratePairs(int amount)
    {
        _cardsInGame = new List<PlayingCard>();

        while (_cardsInGame.Count < amount * 2)
        {
            int randomNumber = Mathf.RoundToInt(Random.Range(1, 14));
            PlayingCardColor randomColor = (PlayingCardColor)Random.Range(0, 2);

            if (randomColor == PlayingCardColor.Red)
            {
                if(!_cardsInGame.Contains(Deck.GetPlayingCard(randomNumber, PlayingCardSuit.Diamonds)))
                {
                    _cardsInGame.Add(Deck.GetPlayingCard(randomNumber, PlayingCardSuit.Diamonds));
                    _cardsInGame.Add(Deck.GetPlayingCard(randomNumber, PlayingCardSuit.Hearts));
                }
            }
            else
            {
                if (!_cardsInGame.Contains(Deck.GetPlayingCard(randomNumber, PlayingCardSuit.Clubs)))
                {
                    _cardsInGame.Add(Deck.GetPlayingCard(randomNumber, PlayingCardSuit.Clubs));
                    _cardsInGame.Add(Deck.GetPlayingCard(randomNumber, PlayingCardSuit.Spades));
                }
            }
        }
    }

    private void SpawnCards()
    {
        foreach (PlayingCard playingCard in _cardsInGame)
        {
            PlayingCard clone = Instantiate(playingCard, transform.position, transform.rotation);
        }
    }

    private static void CheckPair()
    {
        if (_selectedCards[0].Color == _selectedCards[1].Color)
        {
            //DeleteCards();
        }
        else
        {

        }
    }

    private void DeleteCards()
    {
        //Destroy(_cards[_cards.IndexOf(_selectedCards[0])].gameObject);
        //Destroy(_cards[_cards.IndexOf(_selectedCards[1])].gameObject);
    }
    #endregion

    #region Public Methods
    public static void SelectCard(PlayingCard playingCard)
    {
        _selectedCards.Add(playingCard);
        
        if(_selectedCards.Count >= 2)
        {
            CheckPair();
        }
    }
    #endregion
}

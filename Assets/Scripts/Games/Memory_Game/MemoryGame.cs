using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGame : MonoBehaviour
{
    #region Private Properties
    private List<PlayingCard> _cardsInGame;
    private List<PlayingCard> _selectedCards;
    #endregion

    #region Public Properties
    public PlayingCardDeck Deck;
    public int PairAmount;
    #endregion

    #region Private Methods
    private void Start()
    {
        _selectedCards = new List<PlayingCard>();

        this.GeneratePairs(PairAmount);
        this.SpawnCards();
    }

    private void FixedUpdate()
    {
        if(_cardsInGame.Count > 0)
        {
            if (Random.Range(0, 100) > 60)
            {
                int index = Random.Range(0, _cardsInGame.Count);
                if (_cardsInGame[index].Selected == false)
                {
                    _cardsInGame[index].Selected = true;
                }
            }

            for (int i = 0; i < _cardsInGame.Count; i++)
            {
                if (_cardsInGame[i].Selected && !_selectedCards.Contains(_cardsInGame[i]))
                {
                    Debug.Log("Card selected");
                    this.SelectCard(_cardsInGame[i]);
                }
            }
        }
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
        for (int i = 0; i < _cardsInGame.Count; i++)
        {
            PlayingCard clone = Instantiate(_cardsInGame[i], transform.position, transform.rotation);

            _cardsInGame[i] = clone;
        }
    }

    private void CheckPair()
    {
        Debug.Log("Checking pair");
        if (_selectedCards[0].Color == _selectedCards[1].Color && _selectedCards[0].Number == _selectedCards[1].Number && _selectedCards[0] != null)
        {
            Debug.Log("Pair found!");
            this.DeleteCards();
        }
    }

    private void DeleteCards()
    {
        Destroy(_cardsInGame[_cardsInGame.IndexOf(_selectedCards[0])].gameObject);
        _cardsInGame.Remove(_selectedCards[0]);
        Destroy(_cardsInGame[_cardsInGame.IndexOf(_selectedCards[1])].gameObject);
        _cardsInGame.Remove(_selectedCards[1]);
    }

    private void UnSelectCards()
    {
        foreach (PlayingCard playingCard in _selectedCards)
        {
            playingCard.Selected = false;
        }
    }
    #endregion

    #region Public Methods
    public void SelectCard(PlayingCard playingCard)
    {
        _selectedCards.Add(playingCard);
        
        if(_selectedCards.Count >= 2)
        {
            this.CheckPair();
            this.UnSelectCards();
            _selectedCards.Clear();
        }
    }
    #endregion
}
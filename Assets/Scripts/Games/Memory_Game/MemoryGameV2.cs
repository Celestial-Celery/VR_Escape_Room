using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameV2 : MonoBehaviour
{
    #region Private properties
    private List<PlayingCard> _cardsInGame;
    private List<PlayingCard> _selectedCards;
    private Vector3 _nextPosition;

    private bool _gameStarted;

    private bool _cardsSelected;
    private bool _pairChecked;
    private bool _pairFound;
    private bool _cardsRemoved;
    #endregion

    #region Public properties
    public PlayingCardDeck Deck;
    #endregion

    #region Private methods
    private void Start()
    {
        this._selectedCards = new List<PlayingCard>();

        this.GeneratePairs(4);
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
                if (!_cardsInGame.Contains(Deck.GetPlayingCard(randomNumber, PlayingCardSuit.Diamonds)))
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
        this._nextPosition = new Vector3(this.gameObject.transform.localPosition.x, this.gameObject.transform.localPosition.y + 0.5f, this.gameObject.transform.localPosition.z);

        for (int i = 0; i < _cardsInGame.Count; i++)
        {
            PlayingCard clone = Instantiate(_cardsInGame[i], this._nextPosition, transform.rotation);
            clone.transform.parent = this.gameObject.transform;

            _cardsInGame[i] = clone;

            this._nextPosition = new Vector3(this._nextPosition.x + 0.25f, this._nextPosition.y, this._nextPosition.z);
        }
    }

    private void FixedUpdate()
    {
        if (!_gameStarted)
        {
            _gameStarted = true;
            StartCoroutine(GameLogic());
        }
    }

    IEnumerator GameLogic()
    {
        while (_gameStarted)
        {
            yield return new WaitForSeconds(1);

            while (!_cardsSelected)
            {
                this.SelectRandomCard();
                yield return null;
            }

            yield return new WaitForSeconds(1);

            while (!_pairChecked)
            {
                this.CheckPair();
                yield return null;
            }

            yield return new WaitForSeconds(1);

            while (!_cardsRemoved)
            {
                this.RemoveCards();
                yield return null;
            }
        }
    }

    private void SelectRandomCard()
    {
        if (_cardsInGame.Count > 0)
        {
            int index = Random.Range(0, _cardsInGame.Count);
            this.SelectCard(_cardsInGame[index]);
        }
    }

    private void CheckPair()
    {
        Debug.Log("Checking pair");
        if (_selectedCards[0].Color == _selectedCards[1].Color && _selectedCards[0].Number == _selectedCards[1].Number && _selectedCards[0] != null)
        {
            Debug.Log("Pair found!");
            this._pairFound = true;
        }
        else
        {
            Debug.Log("Pair not found!");
            this._pairFound = false;
        }

        this._pairChecked = true;
        this._cardsRemoved = false;
    }

    private void UnSelectCards()
    {
        foreach (PlayingCard playingCard in this._cardsInGame)
        {
            playingCard.Selected = false;
        }
        this._selectedCards = new List<PlayingCard>();
    }

    private void RemoveCards()
    {
        if (_pairFound)
        {
            Debug.Log("Removing pair...");

            Destroy(_cardsInGame[_cardsInGame.IndexOf(_selectedCards[0])].gameObject);
            _cardsInGame.Remove(_selectedCards[0]);
            Destroy(_cardsInGame[_cardsInGame.IndexOf(_selectedCards[1])].gameObject);
            _cardsInGame.Remove(_selectedCards[1]);
        }        

        this.UnSelectCards();

        this._cardsRemoved = true;
        this._cardsSelected = false;
    }
    #endregion

    #region Public methods
    public void SelectCard(PlayingCard playingCard)
    {
        if (!playingCard.Selected && !_selectedCards.Contains(playingCard))
        {
            Debug.Log($"Selected card: {playingCard.Suit} {playingCard.Number}, total cards selected: {_selectedCards.Count + 1}");
            playingCard.Selected = true;
            this._selectedCards.Add(playingCard);

            if (_selectedCards.Count == 2)
            {
                Debug.Log("got 2 cards");
                this._cardsSelected = true;
                this._pairChecked = false;
            }
            else
            {
                this._cardsSelected = false;
            }
        }
    }
    #endregion
}

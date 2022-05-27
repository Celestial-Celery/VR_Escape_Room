using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGame : Game
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

    private float _boardLength;
    private float _boardHeight;
    #endregion

    #region Public properties
    public PlayingCardDeck Deck;
    public int Pairs;
    public int Rotation;
    #endregion

    #region Private methods
    private void Start()
    {
        this.GameState = GameState.InProgress;

        this._boardLength = 0.7f;
        this._boardHeight = 0.24f;
        this._selectedCards = new List<PlayingCard>();

        this.GeneratePairs(Pairs);
        this.ShuffleCards();
        this.SpawnCards();
        this.FixRotation();
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
        int rowCount = Mathf.FloorToInt((Pairs * 2) / 4);
        int rowLength = (Pairs * 2) / rowCount;
        int nextIndex = 0;

        this._nextPosition.z = this.gameObject.transform.localPosition.z + (this._boardHeight / 2);
        this._nextPosition.x = this.gameObject.transform.localPosition.x - (this._boardLength / 2);
        this._nextPosition.y = this.gameObject.transform.localPosition.y + 0.56f;

        for (int rowNr = 0; rowNr < rowCount; rowNr++)
        {
            for (int rowIndex = 0; rowIndex < rowLength; rowIndex++)
            {
                PlayingCard clone = Instantiate(_cardsInGame[nextIndex], this._nextPosition, transform.rotation);
                clone.transform.parent = this.gameObject.transform;

                _cardsInGame[nextIndex] = clone;

                this._nextPosition.x = this._nextPosition.x + (this._boardLength / (rowLength - 1));

                nextIndex += 1;
            }

            this._nextPosition.x = this.gameObject.transform.localPosition.x - (this._boardLength / 2);
            this._nextPosition.z = this._nextPosition.z - (this._boardHeight / (rowCount - 1));
        }
    }

    private void ShuffleCards()
    {
        for (int i = 0; i < this._cardsInGame.Count - 1; i++)
        {
            int rnd = Random.Range(i, this._cardsInGame.Count);
            PlayingCard tempCard = this._cardsInGame[rnd];
            this._cardsInGame[rnd] = this._cardsInGame[i];
            this._cardsInGame[i] = tempCard;
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
                //this.SelectRandomCard();
                this.CheckCards();
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
            _cardsInGame[index].Select();
        }
    }

    private void CheckCards()
    {
        foreach (PlayingCard playingCard in this._cardsInGame)
        {
            if (playingCard.Selected == true)
            {
                this.SelectCard(playingCard);
            }
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

            if (playingCard.IsGameSelected)
            {
                playingCard.Flip();
            }
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

            if (this._cardsInGame.Count == 0)
            {
                this.GameCompleted();
            }
        }

        this.UnSelectCards();

        this._cardsRemoved = true;
        this._cardsSelected = false;
    }

    private void GameCompleted()
    {
        Debug.Log("Memory game completed");
        // Code for game completed here
        this.GameState = GameState.Completed;

        this.SpawnKey();
    }

    private void FixRotation()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, this.Rotation, 0);
    }

    private void SpawnKey()
    {
        Key key = Instantiate(Key, this.transform.position + new Vector3(0, 0.55f, 0), this.transform.rotation);
        key.Door = this.Door;
    }
    #endregion

    #region Public methods
    public void SelectCard(PlayingCard playingCard)
    {
        if (!_selectedCards.Contains(playingCard))
        {
            playingCard.Flip();
            Debug.Log($"Selected card: {playingCard.Suit} {playingCard.Number}, total cards selected: {_selectedCards.Count + 1}");
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

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Deck
{
    private List<CardData> _drawPile;
    private List<CardData> _discardPile = new();
    
    internal int DrawCount => _drawPile.Count;
    internal int DiscardCount => _discardPile.Count;

    internal Deck(List<CardData> startingHand)
    {
        _drawPile = new List<CardData>(startingHand);
        Shuffle(_drawPile);
    }

    internal CardData DrawCard()
    {
        if (_drawPile.Count == 0)
        {
            ReShuffle();
            if (_drawPile.Count == 0) return null;
        }
        
        var card = _drawPile[0];
        _drawPile.RemoveAt(0);
        return card;
    }

    internal void AddToDiscard(CardData card)
    {
        _discardPile.Add(card);
    }

    private void ReShuffle()
    {
        _drawPile.AddRange(_discardPile);
        _discardPile.Clear();
        Shuffle(_drawPile);
    }

    private void Shuffle(List<CardData> cards)
    {
        for (var i = cards.Count - 1; i > 0; i--)
        {
            Debug.Log("at start: ", cards[i]);
            var randomIndex = Random.Range(0, i + 1);
            
            (cards[i], cards[randomIndex]) = (cards[randomIndex], cards[i]);
            Debug.Log("at end: ", cards[i]);
            
        }
        
    }
    
}

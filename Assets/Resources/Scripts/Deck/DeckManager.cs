using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
    // Planned class structure
    
    // Deck class (pure data)
    // stores the draw pile as a List<Card>
    // stores the discard pile as a List<Card>
    // planned methods: Draw() AddToDiscard() ShuffleDiscardIntoDeck()
    
    // HandManager (Monobehaviour)
    // Move cardslots and availablecardslots to it
    // has Disctionary<TowerPlacement, int> to map card to its slot
    // planned methods: TryDrawCard(), DiscardCard(Card card)
    
    // DeckUIManager (Monobehaviour)
    // stores all UI logic related to Decks
    
    // TowerPlacement now has the same function as what i plan Card on having
    // I plan on splitting TowerPlacement into Card and CardPlacement classes

[RequireComponent(typeof(DeckUIManager))]
public class DeckManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private List<CardData> startingDeck;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Transform[] cardSlots;
    
    private Deck _deck;
    private Card[] _activeCards;
    
    private void Start()
    {
        _deck = new Deck(startingDeck);
        _activeCards = new Card[startingDeck.Count];

        for (var i = 0; i < cardSlots.Length; i++)
        {
            DrawToSlot(i);
        }
    }
    
    private void DrawToSlot(int index)
    {
        var data = _deck.DrawCard();
        if (data is null)
        {
            Debug.LogWarning("No card drawn!");
            return;
        }
        
        var card = Instantiate(cardPrefab, cardSlots[index]);
        card.transform.position = cardSlots[index].position;
        
        card.Initialize(data);

        card.OnCardPlayed += HandleCardPlayed;
        card.OnCardDiscarded += HandleCardDiscarded;
        
        _activeCards[index] = card;
    }

    private void HandleCardPlayed(Card card)
    {
        var index = GetCardIndex(card);
        
        _deck.AddToDiscard(card.Data);

        RemoveCard(card, index);
        
        DrawToSlot(index);
    }

    private void HandleCardDiscarded(Card card)
    {
        var index = GetCardIndex(card);
        
        // return card to slot instead of discarding
        card.transform.position = cardSlots[index].position;
    }

    private int GetCardIndex(Card card)
    {
        return Array.IndexOf(_activeCards, card);
    }

    private void RemoveCard(Card card, int index)
    {
        card.OnCardPlayed -= HandleCardPlayed;
        card.OnCardDiscarded -= HandleCardDiscarded;
        
        _activeCards[index] = null;
        
        Destroy(card.gameObject);
    }
    
}

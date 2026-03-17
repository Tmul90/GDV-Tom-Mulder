using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(DeckUIManager))]
public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; private set; }

    [SerializeField] private UnityEvent onInteractCard;
    
    [SerializeField] internal List<TowerPlacement> deck = new();
    [SerializeField] internal Transform[] cardSlots;
    
    private Dictionary<TowerPlacement, int> _cardSlotMap;

    protected DeckUIManager UIManager;
    
    internal bool[] AvailableCardSlots;
    internal List<TowerPlacement> Discarded = new();
    
    
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
    private void Awake()
    {
        Instance = this;
        
        UIManager = GetComponent<DeckUIManager>();
        
        AvailableCardSlots = new bool[cardSlots.Length];
        Array.Fill(AvailableCardSlots, true);
    }
    
    private void Start()
    {
        for (var i = 0; i < cardSlots.Length; i++) { DrawCard(); }
        
        UIManager.ChangeDeckCount(deck.Count);
        UIManager.ChangeDiscardCount(Discarded.Count);
    }

    
    // TODO Find first available slot first then draw a random card.
    public void DrawCard()
    {
        if (deck.Count < 1) return;
        
        var randCard = deck[Random.Range(0, deck.Count)];

        for (var i = 0; i < AvailableCardSlots.Length; i++)
        {
            if (AvailableCardSlots[i] != true) continue;
                
            randCard.gameObject.SetActive(true);
            randCard.handIndex = i;
            randCard.transform.position = cardSlots[i].position;
            AvailableCardSlots[i] = false;
            deck.Remove(randCard);
            UIManager.ChangeDeckCount(deck.Count);
            return;
        }
    }

    public void AddToDiscard(TowerPlacement card)
    {
        Discarded.Add(card); 
        UIManager.ChangeDiscardCount(Discarded.Count);
    }

    // TODO Fisher-Yates shuffle
    // https://en.wikipedia.org/wiki/Fisher–Yates_shuffle
    public void Shuffle()
    {
        if (Discarded.Count < 1) return;
        
        Discarded.ForEach(placement => { deck.Add(placement); });
        
        Discarded.Clear();
    }
}

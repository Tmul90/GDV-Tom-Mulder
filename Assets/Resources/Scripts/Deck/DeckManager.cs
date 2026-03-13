using System;
using System.Collections.Generic;
using Resources.Scripts.Utils;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; private set; }
    [SerializeField] private List<TowerPlacement> deck = new();
    [SerializeField] internal Transform[] cardSlots;
    [SerializeField] internal bool[] availableCardSlots;
    
    // TEMP
    [SerializeField] private TextMeshProUGUI amountInDeckText;
    [SerializeField] private TextMeshProUGUI amountInDiscardText;
    
    private Dictionary<TowerPlacement, int> _cardSlotMap;
    
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
    
    private void Start()
    {
        for (var i = 0; i < cardSlots.Length; i++) { DrawCard(); }
        
        amountInDeckText.text = deck.Count.ToString();
        amountInDiscardText.text = Discarded.Count.ToString();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // TODO move to UI manager script
        amountInDeckText.text = deck.Count.ToString();
        amountInDiscardText.text = Discarded.Count.ToString();
    }
    
    // TODO Find first available slot first then draw a random card.
    public void DrawCard()
    {
        if (deck.Count < 1) return;
        
        var randCard = deck[Random.Range(0, deck.Count)];

        for (var i = 0; i < availableCardSlots.Length; i++)
        {
            if (availableCardSlots[i] != true) continue;
                
            randCard.gameObject.SetActive(true);
            randCard.handIndex = i;
            randCard.transform.position = cardSlots[i].position;
            availableCardSlots[i] = false;
            deck.Remove(randCard);
            return;
        }
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

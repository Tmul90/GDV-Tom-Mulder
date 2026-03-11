using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;
using Util;

public class Stats : MonoBehaviour
{
    // TODO split entire script into smaller scripts
    // TODO make energy manager
    public float energy
    {
        get => _energy;
        private set => _energy = value;
    }
    
    [SerializeField] private TextMeshProUGUI textmeshproHealth;
    [SerializeField] private TextMeshProUGUI textmeshproEnergy;
    
    private static float _energy = 50f;
    
    private float _lives = 100;
    
    // TODO create deck manager script move this into that
    [SerializeField] private List<TowerPlacement> deck = new List<TowerPlacement>();
    public List<TowerPlacement> discarded = new List<TowerPlacement>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;
    
    [SerializeField] private GameObject amountInDeck;
    TextMeshProUGUI amountInDeckText;

    [SerializeField] private GameObject amountInDiscard;
    TextMeshProUGUI amountInDiscardText;
    // ----------------------------------------------------
    private void Start()
    {
        for (var i = 0; i < cardSlots.Length; i++)
        {
            DrawCard();
        }
        
        // TODO there is no need for this to be inside of Stats move to other script
        textmeshproHealth.text = "Health: " + _lives.ToString();

        amountInDeckText = amountInDeck.GetComponent<TextMeshProUGUI>();
        amountInDeckText.text = deck.Count.ToString();

        amountInDiscardText = amountInDiscard.GetComponent<TextMeshProUGUI>();
        amountInDiscardText.text = discarded.Count.ToString();
        
        textmeshproHealth.text = "Energy: " + energy.ToString();
        // ------------------------------------------------------------------------------
    }
    private void Update()
    {
        // TODO move to different script
        amountInDeckText.text = deck.Count.ToString();
        amountInDiscardText.text = discarded.Count.ToString();
        textmeshproHealth.text = "Energy: " + _energy;
        // -----------------------------------------------------
        
        // TODO Make _energy MinMax<>
        if (energy > 100)
        {
            energy = 100;
        }
    }

    public void EnemyThrough(float TypeDamage)
    {
        _lives -= TypeDamage;
        // TODO move to different script
        textmeshproHealth.text = "Health: " + _lives;
        // -----------------------------------------------------
    }
    public static void EnergyDeplete(float Cost) { _energy -= Cost; }

    public static void EnemyKill(float energyGain)
    {
        if (_energy < 100)
        {
            _energy += energyGain;
        }
        
    }

    public static void ChangeEnergy(float EnergyMultiplier) { _energy *= EnergyMultiplier; }
    
    public void DrawCard()
    {
        if (deck.Count < 1) return;
        
        var randCard = deck[Random.Range(0, deck.Count)];

        for (var i = 0; i < deck.Count; i++)
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

    public void Shuffle()
    {
        if (discarded.Count < 1) return;
        
        discarded.ForEach(placement => { deck.Add(placement); });
        
        discarded.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Dictionary<string, CardData> _deckDictDict = new();

    public Dictionary<string, CardData> DeckDict
    {
        get => _deckDictDict; 
        set => _deckDictDict = value;
    }
}

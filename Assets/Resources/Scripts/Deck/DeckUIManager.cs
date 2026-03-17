using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountInDeckText;
    [SerializeField] private TextMeshProUGUI amountInDiscardText;
    
    public void ChangeDeckCount(int count) { amountInDeckText.text = count.ToString(); }

    public void ChangeDiscardCount(int count) { amountInDiscardText.text = count.ToString(); }

}

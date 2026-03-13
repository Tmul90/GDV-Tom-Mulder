using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TowerPlacement : MonoBehaviour
{
    private bool canBePlayed = true;
    private bool wasPlaced;
    public int handIndex;

    private float CardCost;

    public enum CardType {High, Medium, Low};
    public CardType Cardtype;

    // TODO SUPER ULTRA TEMPORARY
    private DeckManager deckManager;

    [SerializeField] GameObject Outline;
    [SerializeField] GameObject Tower;
    [SerializeField] GameObject TowerModel;
    private Vector3 originalPos;
    public Vector2 originalCardLoc;


    private void Start()
    {
        // super temporary and ugly code will be changed once ive made the Card class
        switch (Cardtype)
        {
            case CardType.High:
                SetCardCost(50);
                break;
            case CardType.Medium:
                SetCardCost(20);
                break;
            default:
                SetCardCost(10);
                break;
        }
        
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        
        // TODO remove direct reference to DeckManager with maybe events
        deckManager = GameObject.FindGameObjectWithTag("Stats").GetComponent<DeckManager>();

    }
    private void OnMouseDrag()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (Stats.Energy > CardCost)
        {
            transform.Translate(mousePosition);
            
            // TODO move to UI class
            Outline.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0);
        }
        else gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1);
    }
    private void OnMouseEnter()
    {
        Outline.GetComponent<SpriteRenderer>().color = Stats.Energy > CardCost ? new Color(0, 0, 0, 1) : new Color(255, 0, 0, 1);
    }
    private void OnMouseExit()
    {
        Outline.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }
    private void OnMouseUp()
    {
        if (!(Stats.Energy > CardCost)) return;
        
        if(canBePlayed)
        {
            var pp = Instantiate(Tower);
            pp.transform.position = transform.position;
                
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            gameObject.transform.position = originalPos;
            deckManager.availableCardSlots[handIndex] = true;
            deckManager.DrawCard();
            Stats.EnergyDeplete(CardCost);
            Invoke("MoveToDiscard", 1f);
        }
        else { transform.position = deckManager.cardSlots[handIndex].transform.position; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canBePlayed = false;
        Debug.Log(canBePlayed);
        TowerModel.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canBePlayed = true;
        Debug.Log(canBePlayed);
        TowerModel.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);

    }

    private void MoveToDiscard()
    {
        deckManager.Discarded.Add(this);
        gameObject.SetActive(false);
    }
    
    // TODO move to cost to card
    private void CardTypeHigh()
    {
        CardCost = 50;
    }
    private void CardTypeMedium()
    {
        CardCost = 20;
    }
    private void CardTypeLow()
    {
        CardCost = 10;
    }

    private void SetCardCost(float _cardCost) { CardCost = _cardCost; }
}

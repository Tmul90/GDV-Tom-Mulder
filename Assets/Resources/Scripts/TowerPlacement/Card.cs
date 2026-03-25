using System;
using UnityEngine;

[RequireComponent(typeof(TowerPlacementValidator))]
public class TowerPlacement : MonoBehaviour
{
    // No need to stay here move to HandManager
    public int handIndex;

    public event Action<Card> OnCardPlayed;
    public event Action<Card> OnCardDiscarded;
    
    // Need to be managed around the deck system
    [SerializeField] private CardData cardData;
    
    private SpriteRenderer _spriteRenderer;
    private bool _isDragging;

    private TowerPreview _activeTowerPreview;
    private TowerPlacementValidator TowerValidator { get; set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        TowerValidator = GetComponent<TowerPlacementValidator>();
    }

    private void OnMouseDown()
    {
        var previewObject = Instantiate(cardData.towerPrefab);
        _activeTowerPreview = previewObject.GetComponent<TowerPreview>();
        
        _spriteRenderer.enabled = false;
    }
    
    private bool CanPlace => TowerValidator is not null 
                             && TowerValidator.IsValid 
                             && Stats.Instance.GetEnergy() > cardData.energyMultiplier;

    private void OnMouseDrag()
    {
        if (!Camera.main) return;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var previewPosition = new Vector3(mousePosition.x, mousePosition.y, -1f);

        _isDragging = true;
        
        mousePosition.z = 0f;
        transform.position = mousePosition;

        if (_activeTowerPreview is null) return;
        
        _activeTowerPreview.transform.position = previewPosition;
        
        TowerValidator.transform.position = previewPosition;
        _activeTowerPreview.UpdateState(CanPlace);
    }
    private void OnMouseUp()
    {
        var shouldPlace = CanPlace;
        
        _isDragging = false;

        if (_activeTowerPreview is not null)
        {
            Destroy(_activeTowerPreview.gameObject);
        }

        if (!shouldPlace)
        {
            ReturnToHand(); 
            return;
        }

        PlaceTower();
    }
    private void OnMouseEnter()
    {
        if (_isDragging) return;
        
        var canAfford = Stats.Instance.GetEnergy() > cardData.energyMultiplier;
        
        _spriteRenderer.color = canAfford ? new Color(1f, 1f, 0f, 1f) : new Color(1f, 0f, 0f, 1f);
    }

    private void OnMouseExit()
    {
        if (_isDragging) return;
        _spriteRenderer.color = Color.white;
    }

    private void PlaceTower()
    {
        if (!Camera.main) return;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var tower = Instantiate(cardData.towerPrefab, new Vector3(mousePosition.x, mousePosition.y, -1f), Quaternion.identity);
        
        tower.GetComponent<TowerPreview>()?.SetPlaced();
        
        _spriteRenderer.color = Color.white;

        Stats.Instance.EnergyDeplete(cardData.energyMultiplier);
        OnCardPlayed?.Invoke(this);
    }
    
    private void ReturnToHand()
    {
        _spriteRenderer.enabled = true;
        // TODO: link to event
        transform.position = DeckManager.Instance.cardSlots[handIndex].transform.position;
        _spriteRenderer.color = Color.white;
    }

    private void MoveToDiscard()
    {
        // TODO: link to event
        OnCardDiscarded?.Invoke(this);
        gameObject.SetActive(false);
    }
}

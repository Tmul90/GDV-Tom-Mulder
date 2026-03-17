using UnityEngine;

[RequireComponent(typeof(TowerPlacementValidator))]
public class TowerPlacement : MonoBehaviour
{
    // No need to stay here move to HandManager
    public int handIndex;
    
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
                             && Stats.Energy > cardData.energyCost;

    private void OnMouseDrag()
    {
        _isDragging = true;

        if (!Camera.main) return;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        transform.position = mousePosition;

        if (_activeTowerPreview is null) return;
        
        var previewPosition = new Vector3(mousePosition.x, mousePosition.y, -1f);
        _activeTowerPreview.transform.position = previewPosition;
        
        TowerValidator.transform.position = previewPosition;
        _activeTowerPreview.UpdateState(CanPlace);
    }
    private void OnMouseUp()
    {
        _isDragging = false;
        
        var shouldPlace = CanPlace; 
        
        if (_activeTowerPreview is not null) { Destroy(_activeTowerPreview.gameObject); }

        if (!shouldPlace) { ReturnToHand(); return; }

        PlaceTower();
    }
    private void OnMouseEnter()
    {
        if (_isDragging) return;
        
        var canAfford = Stats.Energy > cardData.energyCost;
        
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
        DeckManager.Instance.AvailableCardSlots[handIndex] = true;
        DeckManager.Instance.DrawCard();
        Stats.EnergyDeplete(cardData.energyCost);
        Invoke(nameof(MoveToDiscard), 1f);
    }
    
    private void ReturnToHand()
    {
        _spriteRenderer.enabled = true;
        transform.position = DeckManager.Instance.cardSlots[handIndex].transform.position;
        _spriteRenderer.color = Color.white;
    }

    private void MoveToDiscard()
    {
        DeckManager.Instance.AddToDiscard(this);
        gameObject.SetActive(false);
    }
}

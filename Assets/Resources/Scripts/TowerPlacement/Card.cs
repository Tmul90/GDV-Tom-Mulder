using System;
using UnityEngine;

[RequireComponent(typeof(TowerPlacementValidator))]
[RequireComponent(typeof(BoxCollider2D))]
public class Card : MonoBehaviour
{
    internal CardData Data { get; private set; }

    internal event Action<Card> OnCardPlayed;
    internal event Action<Card> OnCardDiscarded;
    
    private SpriteRenderer _spriteRenderer;
    private TowerPlacementValidator _towerPlacementValidator;
    private TowerPreview _activeTowerPreview;

    private Camera _camera;
    private bool _isDragging;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _towerPlacementValidator = GetComponent<TowerPlacementValidator>();
        _camera = Camera.main;
    }
    
    public void Initialize(CardData data)
    {
        Data = data;
        
        // According to Rider this is the same as if (_spriteRenderer == null) thats cool to know
        _spriteRenderer ??= GetComponent<SpriteRenderer>();

        _spriteRenderer.sprite = data.cardArt;
        _spriteRenderer.sortingOrder = 1;
    }

    private bool CanPlaceTower => _towerPlacementValidator != null && _towerPlacementValidator.IsValid && Stats.Instance.GetEnergy() > Data.energyMultiplier;
    
    private void OnMouseDown()
    {
        var previewObject = Instantiate(Data.towerPrefab);
        _activeTowerPreview = previewObject.GetComponent<TowerPreview>();
        
        _spriteRenderer.enabled = false;
    }

    private void OnMouseDrag()
    {
        if (!Camera.main) return;
        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        
        transform.position = mousePosition;
        
        if (_activeTowerPreview is null) return;
        
        var previewPosition = new Vector3(mousePosition.x, mousePosition.y, -1f);
        
        _activeTowerPreview.transform.position = previewPosition;
        _towerPlacementValidator.transform.position = previewPosition;
        
        _activeTowerPreview.UpdateState(CanPlaceTower);
        _isDragging = true;
    }
    private void OnMouseUp()
    {
        var shouldPlace = CanPlaceTower;
        
        _isDragging = false;

        if (_activeTowerPreview is not null)
        {
            Destroy(_activeTowerPreview.gameObject);
        }

        if (!shouldPlace)
        {
            OnCardDiscarded?.Invoke(this);
            return;
        }

        PlaceTower();
    }
    private void OnMouseEnter()
    {
        if (_isDragging) return;
        
        var canAfford = Stats.Instance.GetEnergy() > Data.energyMultiplier;
        
        _spriteRenderer.color = canAfford ? Color.yellow : Color.red;
    }

    private void OnMouseExit()
    {
        if (_isDragging) return;
        _spriteRenderer.color = Color.white;
    }

    private void PlaceTower()
    {
        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        var tower = Instantiate(
            Data.towerPrefab, 
            new Vector3(mousePosition.x, mousePosition.y, -1f), 
            Quaternion.identity
            );
        
        tower.GetComponent<TowerPreview>()?.SetPlaced();

        Stats.Instance.EnergyDeplete(Data.energyMultiplier);
        
        OnCardPlayed?.Invoke(this);
    }
}

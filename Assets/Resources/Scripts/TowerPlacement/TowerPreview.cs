using System;
using UnityEngine;

public class TowerPreview : MonoBehaviour
{
    [SerializeField] private SpriteRenderer modelRenderer;
    [SerializeField] private float outlineSize = 1.1f;
    
    private SpriteRenderer _outlineRenderer;
    
    private static readonly Color ValidColor    = new Color(0f, 1f, 0f, 0.6f);
    private static readonly Color InvalidColor  = new Color(1f, 0f, 0f, 0.6f);
    private static readonly Color OutlineOn     = new Color(1f, 1f, 1f, 1f);
    private static readonly Color OutlineOff    = new Color(1f, 1f, 1f, 0f);

    private void Awake()
    {
        _outlineRenderer = CreateOutlineRenderer();
        _outlineRenderer.enabled = false;
    }

    private SpriteRenderer CreateOutlineRenderer()
    {
        var outlineObject = new GameObject("Outline");
        outlineObject.transform.SetParent(transform);
        outlineObject.transform.localPosition = Vector3.zero;
        outlineObject.transform.localScale = Vector3.one * outlineSize;
        
        var spriteRenderer = outlineObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = modelRenderer.sprite;
        spriteRenderer.sortingLayerID = modelRenderer.sortingLayerID;
        spriteRenderer.sortingOrder = modelRenderer.sortingOrder - 1;
        spriteRenderer.color = Color.white;
        
        return spriteRenderer;
    }

    public void UpdateState(bool isValid)
    {
        modelRenderer.color = isValid ? ValidColor : InvalidColor;
        _outlineRenderer.color = isValid ? OutlineOn : OutlineOff;
    }

    public void SetPlaced()
    {
        modelRenderer.color = Color.white;
        _outlineRenderer.color = OutlineOff;
    }
}

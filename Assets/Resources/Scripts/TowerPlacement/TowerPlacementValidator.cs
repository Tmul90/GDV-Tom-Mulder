using UnityEngine;

public class TowerPlacementValidator : MonoBehaviour
{
    [SerializeField] private float validRadius = 0.5f;
    [SerializeField] private LayerMask blockerLayers;

    public bool IsValid => !IsBlocked();

    private bool IsBlocked()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, validRadius, blockerLayers);
        return hits.Length > 0;
    }
}

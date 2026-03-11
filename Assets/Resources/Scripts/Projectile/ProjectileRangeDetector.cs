using System;
using UnityEngine;

public class ProjectileRangeDetector : MonoBehaviour
{
    public event Action<GameObject> OnEnemyHit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnEnemyHit?.Invoke(collision.gameObject);
    }
}

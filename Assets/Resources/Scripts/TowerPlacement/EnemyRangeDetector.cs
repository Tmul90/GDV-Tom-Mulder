using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeDetector : MonoBehaviour
{
    public List<GameObject> EnemiesInRange { get; } = new();
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) { EnemiesInRange.Add(collision.gameObject); }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) { EnemiesInRange.Remove(collision.gameObject); }
    }
}

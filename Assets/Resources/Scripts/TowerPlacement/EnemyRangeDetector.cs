using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyRangeDetector : MonoBehaviour
{
    internal List<GameObject> EnemiesInRange { get; } = new();


    private void Update()
    {
        print(EnemiesInRange.Count);
    }

    // TODO fix collider issues
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered: ", collision.gameObject);
        if (collision.gameObject.CompareTag("Enemy")) { EnemiesInRange.Add(collision.gameObject); }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exited: ", collision.gameObject);
        if (collision.gameObject.CompareTag("Enemy")) { EnemiesInRange.Remove(collision.gameObject); }
    }
}

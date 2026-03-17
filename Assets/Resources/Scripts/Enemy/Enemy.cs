using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyPathfinder))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> isEnemyTrough;
    protected abstract float Damage { get; }
    protected abstract float Speed { get; }
    protected abstract float MaxHealth { get; }
    protected abstract float EnergyDrop { get; }
    protected abstract int Path { get; }
    
    private float _currentHealth;
    private bool _isDead;
    
    private void Start()
    {
        _currentHealth = MaxHealth;

        var pathfinder = GetComponent<EnemyPathfinder>();
        pathfinder.SetSpeed(Speed);
        pathfinder.SetPath(Path);
        pathfinder.PathFinder();
    }

    public void TakeDamage(float damageTaken)
    {
        _currentHealth -= damageTaken;
        print(_currentHealth);
        if(_currentHealth >= 0) return;
        
        _isDead = true;
        Destroy(gameObject);
        Stats.EnemyKill(EnergyDrop);
    }

    public void AtEnd()
    {
        if(_isDead) return;
        _isDead = true;
        
        Stats.Instance.TakeDamage(Damage);
        isEnemyTrough?.Invoke(Damage);
        Destroy(gameObject);
    }
}

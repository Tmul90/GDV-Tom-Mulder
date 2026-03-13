using System;
using UnityEngine;

[RequireComponent(typeof(EnemyPathfinder))]
public abstract class Enemy : MonoBehaviour
{
    public event Action<float> IsEnemyTrough;
    protected EnemyPathfinder EnemyPathfinder { get; private set; }

    protected abstract float Damage { get; }
    
    protected abstract float Speed { get;  }
    protected abstract float Health { get; set; }
    protected abstract float EnergyDrop { get;  }

    internal float _health;
    
    private void Start()
    {
        EnemyPathfinder.AtEnd += AtEnd;
        EnemyPathfinder.PathFinder();
    }

    private void Update()
    {
        // TODO event
        if (!(Health < 0)) return;
        
        Destroy(this);
        Stats.EnemyKill(EnergyDrop);
        // --------------------------------
    }
    
    protected internal void TakeDamage(float damageTaken) { Health -= damageTaken; }

    protected virtual void AtEnd()
    {
        Destroy(this);
        
        // TODO move into the IsEnemyThrough action
        /*_stats.EnemyThrough(_typeDamage);*/
        Stats.TakeDamage(Damage);
        IsEnemyTrough?.Invoke(Damage);
    }
    
    /*private void FootSoldier()
    {
        _enemySpeed = 3;
        _enemyHealth = 100;
        _damage = 1;
        _enemyEnergyDrop = 2;
    }

    private void Cavalry()
    {
        _enemySpeed = 6;
        _enemyHealth = 50;
        _damage = 2;
        _enemyEnergyDrop = 3;
    }*/
}

using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(StatsUIManager))]
public class Stats : MonoBehaviour
{
    public static Stats Instance { get; private set; }
    
    [SerializeField] private UnityEvent onHealthChanged;
    [SerializeField] private UnityEvent onEnergyChanged;
    
    // TODO split entire script into smaller scripts
    // TODO make energy manager
    
    private static float _energy = 50f;
    private static float _health = 100;

    private void Awake()
    {
        Instance = this;
        onHealthChanged.Invoke();
        onEnergyChanged.Invoke();
    }
    private void Update()
    {
        // TODO Make _energy MinMax<>
        if (_energy > 100)
        {
            _energy = 100;
        }
    }

    public float GetEnergy()
    {
        return _energy;
    }

    public float GetHealth()
    {
        return _health;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage; onHealthChanged.Invoke();
    }

    public void EnergyDeplete(float Cost)
    {
        _energy -= Cost; onEnergyChanged.Invoke();
    }

    public void EnemyKill(float energyGain)
    {
        if (!(_energy < 100)) return;
        
        _energy += energyGain;
        onEnergyChanged.Invoke();
    }

}

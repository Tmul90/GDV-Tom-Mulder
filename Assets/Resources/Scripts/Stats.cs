using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    // TODO split entire script into smaller scripts
    // TODO make energy manager
    public static float Energy
    {
        get => _energy;
        private set => _energy = value;
    }

    public float Lives
    {
        get => _lives;
        private set => _lives = value;
    }
    
    [SerializeField] private TextMeshProUGUI textmeshproHealth;
    [SerializeField] private TextMeshProUGUI textmeshproEnergy;
    
    private static float _energy = 50f;
    private static float _lives = 100;
    
    // TODO create deck manager script move this into that

    // ----------------------------------------------------
    private void Start()
    {

        
        // TODO there is no need for this to be inside of Stats move to other script
        textmeshproHealth.text = "Health: " + _lives.ToString();


        
        textmeshproHealth.text = "Energy: " + Energy.ToString();
        // ------------------------------------------------------------------------------
    }
    private void Update()
    {
        textmeshproHealth.text = "Health: " + _lives;
        
        // TODO move to different script

        textmeshproHealth.text = "Energy: " + _energy;
        // -----------------------------------------------------
        
        // TODO Make _energy MinMax<>
        if (Energy > 100)
        {
            Energy = 100;
        }
    }


    // TODO event
    public static void TakeDamage(float damage) { _lives -= damage; }
    
    // TODO event
    public static void EnergyDeplete(float Cost) { _energy -= Cost; }

    public static void EnemyKill(float energyGain)
    {
        if (_energy < 100)
        {
            _energy += energyGain;
        }
        
    }

    public static void ChangeEnergy(float energyMultiplier) { _energy *= energyMultiplier; }
}

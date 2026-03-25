using System;
using TMPro;
using UnityEngine;

public class StatsUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI energyText;

    public void UpdateHealth()
    {
        healthText.text = "Health: " + Stats.Instance.GetHealth().ToString();
    }

    public void UpdateEnergy()
    {
        healthText.text = "Energy: " + Stats.Instance.GetEnergy().ToString();
    }
}

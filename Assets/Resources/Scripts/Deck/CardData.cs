using Resources.Scripts.Towers;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Cards/CardData")]
public class CardData : ScriptableObject
{
    public TowerBase towerPrefab;
    public Sprite cardArt;
    public float energyCost;
    public string cardName;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavalryEnemy : Enemy
{
    protected override float Damage => 30f;
    protected override float Speed { get; }
    protected override float Health
    {
        get => 50f;
        set { }
    }

    protected override float EnergyDrop { get; }
}

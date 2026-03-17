public class CavalryEnemy : Enemy
{
    protected override float Speed      => 6f;
    protected override float MaxHealth  => 50f;
    protected override float Damage => 2f;
    protected override float EnergyDrop => 3f;
    protected override int Path => 0;
}

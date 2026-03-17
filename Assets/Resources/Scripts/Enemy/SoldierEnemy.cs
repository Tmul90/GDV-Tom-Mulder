public class SoldierEnemy : Enemy
{
    protected override float Speed      => 3f;
    protected override float MaxHealth  => 100f;
    protected override float Damage => 1f;
    protected override float EnergyDrop => 2f;
    protected override int Path => 0;
}

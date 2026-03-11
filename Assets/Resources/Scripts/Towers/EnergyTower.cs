public class EnergyTower : TowerBase
{
    protected override float Delay => 0f;
    
    protected override void Shoot() { Stats.ChangeEnergy(1.2f); }
}

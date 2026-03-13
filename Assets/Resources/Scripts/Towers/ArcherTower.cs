namespace Resources.Scripts.Towers
{
    public class ArcherTower : TowerBase
    {
        protected override float Delay => 1.2f;
    
        protected override void Shoot()
        {
            Launcher.Fire(RangeDetector.EnemiesInRange[0]);
        }
    }
}

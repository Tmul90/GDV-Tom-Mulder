namespace Resources.Scripts.Towers
{
    public class ArcherTower : TowerBase
    {
        protected override float Delay => 1.2f;
    
        /// <summary>
        /// Instantiates a projectile without passing true or false so the projectile defaults to False
        /// making this NOT a BOMB(oclaat)
        /// </summary>
        protected override void Shoot()
        {
            Launcher.Fire(RangeDetector.EnemiesInRange[0]);
        }
    }
}

namespace Resources.Scripts.Towers
{
    public class BombTower : TowerBase
    {
        protected override float Delay => 1f;

        private void Start()
        {
            base.Start();
            Launcher.OnProjectileFired += (projectile, target) =>
            {
                projectile.GetComponent<ProjectileScript>().doesExplosion = true;
            };
        }
        
        protected override void Shoot() { Launcher.Fire(RangeDetector.EnemiesInRange[0]); }
    }
}

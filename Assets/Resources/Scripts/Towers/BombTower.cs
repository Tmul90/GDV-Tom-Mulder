using UnityEngine;

namespace Resources.Scripts.Towers
{
    public class BombTower : TowerBase
    {
        // BOMBoclaat tower
        protected override float Delay => 1f;
        
        protected override void Shoot()
        {
            Launcher.Fire(RangeDetector.EnemiesInRange[0], doesExplosion: true);
        }
    }
}

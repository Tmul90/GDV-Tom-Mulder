using UnityEngine;

namespace Resources.Scripts.Towers
{
    public class BombTower : TowerBase
    {
        // TODO fix tower not shooting
        protected override float Delay => 1f;
        
        protected override void Shoot() { Launcher.Fire(RangeDetector.EnemiesInRange[0]); }
        
        public void EnableExplosion() { Launcher.DoesExplosion(true); }
    }
}

using UnityEngine;

namespace Resources.Scripts.Towers
{
    public class EnergyTower : TowerBase
    {
        [SerializeField] private float _energyGain = 10;
        protected override float Delay => 0f;

        /// <summary>
        /// instead of instantiating a bomboclaat projectile it generates energy
        /// </summary>
        protected override void Shoot()
        {
            Stats.Instance.EnergyDeplete(-_energyGain);
        }
    }
}

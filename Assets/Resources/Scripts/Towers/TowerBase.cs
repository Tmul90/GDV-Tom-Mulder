using UnityEngine;

namespace Resources.Scripts.Towers
{
    [RequireComponent(typeof(EnemyRangeDetector))]
    [RequireComponent(typeof(ProjectileLauncher))]
    public abstract class TowerBase : MonoBehaviour
    {
        protected EnemyRangeDetector RangeDetector { get; private set; }
        protected ProjectileLauncher Launcher { get; private set; }
        protected abstract float Delay  { get; }
    
        private float _nextShot;
    
        protected virtual void Start()
        {
            RangeDetector = GetComponent<EnemyRangeDetector>();
            Launcher = GetComponent<ProjectileLauncher>();
        }

        protected virtual void Update()
        {
            if (RangeDetector.EnemiesInRange.Count <= 0 || !(Time.time >= _nextShot)) { return; }

            Shoot();
        
            _nextShot = Time.time + Delay;
        }

        protected abstract void Shoot();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ProjectileRangeDetector))]
public class ProjectileScript : MonoBehaviour
{
    public static event Action<ProjectileScript> OnProjectileCreated;
    
    public GameObject curTarget;
    public bool doesExplosion;
    
    [SerializeField] private float speed = 10f;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float lifeTime = 0.5f;
    
    private Vector3 _projDir;

    private void Start()
    {
        _projDir = (curTarget.transform.position - transform.position).normalized;
        
        var detector = GetComponent<ProjectileRangeDetector>();
        detector.OnEnemyHit += enemy =>
        {
            if (doesExplosion) { InstantiateExplosion(); }
            else { enemy.gameObject.GetComponent<Enemy>().Damage(30); }
        };
        
        OnProjectileCreated?.Invoke(this);
    }
    private void Update() { transform.Translate(_projDir * speed * Time.deltaTime); }
    private void InstantiateExplosion()
    {
        var enemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, LayerMask.GetMask("EnemyLayer"));
        Array.ForEach(enemies, e => e.GetComponent<Enemy>().Damage(15));
        
        var explosion = Instantiate(FileUtils.LoadPrefab("ExplosionRadius"), transform.position, Quaternion.identity);
        Destroy(explosion, lifeTime);
        Destroy(gameObject);
    }
}

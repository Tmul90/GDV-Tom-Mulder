using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private UnityEvent onProjectileFired;
    
    private GameObject _bulletPrefab;
    private const float LifeTime = 0.5f;
    private GameObject _projectilePrefab;

    private void Start()
    {
        _bulletPrefab = FileUtils.LoadPrefab("Bullet");
    }

    internal void Fire(GameObject target, bool doesExplosion = false)
    {
        onProjectileFired?.Invoke();
        
        _projectilePrefab = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        var projectile = _projectilePrefab.GetComponent<ProjectileScript>();
        projectile.curTarget = target;
        projectile.doesExplosion = doesExplosion;
        
        Destroy(_projectilePrefab, LifeTime);
    }
}

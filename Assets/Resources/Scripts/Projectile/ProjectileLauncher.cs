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

    private void Start() { _bulletPrefab = FileUtils.LoadPrefab("Bullet"); }

    public void Fire(GameObject target)
    {
        onProjectileFired?.Invoke();
        
        _projectilePrefab = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        _projectilePrefab.GetComponent<ProjectileScript>().curTarget = target;
        
        Destroy(_projectilePrefab, LifeTime);
    }

    public void DoesExplosion(bool doesExplosion) { _projectilePrefab.GetComponent<ProjectileScript>().doesExplosion = doesExplosion; }
}

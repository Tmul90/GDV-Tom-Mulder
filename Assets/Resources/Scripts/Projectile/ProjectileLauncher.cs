using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public event Action<GameObject, GameObject> OnProjectileFired;
    
    private GameObject _bulletPrefab;
    private const float LifeTime = 0.5f;

    private void Start() { _bulletPrefab = FileUtils.LoadPrefab("Bullet"); }

    public void Fire(GameObject target)
    {
        var projectile = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<ProjectileScript>().curTarget = target;
        
        OnProjectileFired?.Invoke(projectile, target);
        
        Destroy(projectile, LifeTime);
    }
}

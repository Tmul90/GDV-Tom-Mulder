using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Waypoint Variables
    [SerializeField] private int at = 0;
    [SerializeField] private Vector3 target;
    private readonly List<Transform> _waypoints = new();
    private int _path;
    private Transform _coords;

    // Type Variables
    private enum EnemyTypes{FootSoldier, Cavalry};
    [SerializeField] private EnemyTypes enemyType;
    private float _typeDamage;

    // Enemy Variables
    private float _enemySpeed;
    private float _enemyHealth;
    private float _enemyEnergyDrop;

    private Stats _stats;
    private bool _isDead;
    
    private void Start()
    {
        switch (enemyType)
        {
            case EnemyTypes.FootSoldier:
                FootSoldier();
                break;
            case EnemyTypes.Cavalry:
                Cavalry();
                break;
        }

        PathFinder();

        _stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
    }

    private void Update()
    {
        if (transform.position != _waypoints[at].transform.position)
        {
            target = _waypoints[at].transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * _enemySpeed);
        }
        else if (_waypoints.Count > at + 1) at++;

        else AtEnd();

        if (!(_enemyHealth < 0)) return;
        
        Destroy(gameObject);
        Stats.EnemyKill(_enemyEnergyDrop);
    }

    private void PathFinder()
    {
        _coords = GameObject.Find("Waypoints " + _path).transform;
        for (int i = 0; i < _coords.childCount; i++) _waypoints.Add(_coords.GetChild(i).transform);
    }

    private void FootSoldier()
    {
        _enemySpeed = 3;
        _enemyHealth = 100;
        _path = 0;
        _typeDamage = 1;
        _enemyEnergyDrop = 2;
    }

    private void Cavalry()
    {
        _enemySpeed = 6;
        _enemyHealth = 50;
        _path = 0;
        _typeDamage = 2;
        _enemyEnergyDrop = 3;
    }
    public void Damage(float Damage)
    {
        _enemyHealth -= Damage;
        Debug.Log(_enemyHealth);
    }

    private void AtEnd()
    {
        if (_isDead) return;


        _isDead = true;
        _stats.EnemyThrough(_typeDamage);
        Destroy(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinder : MonoBehaviour
{
    public event Action AtEnd;
    
    [SerializeField] private int at;

    private float _speed;
    private readonly List<Transform> _waypoints = new();
    private int _path;
    
    private Stats _stats;


    private void Update()
    {
        if (transform.position != _waypoints[at].transform.position)
        {
            var target = _waypoints[at].transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * _speed);
        }
        else if (_waypoints.Count > at + 1) at++;
        
        AtEnd?.Invoke();
    }

    internal void PathFinder()
    {
        var coords = GameObject.Find("Waypoints " + _path).transform;
        for (var i = 0; i < coords.childCount; i++) _waypoints.Add(coords.GetChild(i).transform);
    }
}

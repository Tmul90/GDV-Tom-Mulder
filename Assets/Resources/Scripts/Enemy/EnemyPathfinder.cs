using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPathfinder : MonoBehaviour
{
    [SerializeField] private UnityEvent atEnd;
    
    [SerializeField] private List<Transform> waypoints = new();
    
    private int _at;
    private float _speed;
    private int _path;
    private Stats _stats;

    private void Update()
    {
        if (transform.position != waypoints[_at].transform.position)
        {
            var target = waypoints[_at].transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * _speed);
        }
        else if (waypoints.Count > _at + 1) { _at++; }
        else { atEnd?.Invoke(); }
        
    }

    internal void PathFinder()
    {
        var coords = GameObject.Find("Waypoints " + _path).transform;
        for (var i = 0; i < coords.childCount; i++) waypoints.Add(coords.GetChild(i).transform);
    }
    
    internal void SetPath(int path) => _path = path;
    
    internal void SetSpeed(float speed) => _speed = speed;
}

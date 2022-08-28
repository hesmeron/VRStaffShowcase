using System;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _startWaypoints = Array.Empty<Transform>();
    [SerializeField] private float _completionDistance = 0.05f;
    private Queue<Transform> _waypointsQueue = new Queue<Transform>();

    private void OnDrawGizmosSelected()
    {
        foreach (var waypoint in _startWaypoints)
        {
            Gizmos.DrawSphere(waypoint.position, _completionDistance);           
        }
    }

    private void Start()
    {
        foreach (var waypoint in _startWaypoints)
        {
            _waypointsQueue.Enqueue(waypoint);
        }
    }

    private void Update()
    {
        if (_waypointsQueue.Count > 0)
        {
            Vector3 direction = _waypointsQueue.Peek().transform.position - transform.position;
            transform.position += direction.normalized * (_speed * Time.deltaTime);
            if (direction.magnitude <= _completionDistance)
            {
                _waypointsQueue.Dequeue();
            }
        }
    }
}

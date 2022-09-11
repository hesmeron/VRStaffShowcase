using System;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower
{
    private float _speed;
    private float _completionDistance;
    private Queue<Transform> _waypointsQueue = new Queue<Transform>();
    private Movement _movement;
    
    public WaypointFollower(Transform transform, float _speed, float completionDistance)
    {
        _completionDistance = completionDistance;
        _movement = new Movement(new MovementData(transform, _speed));
    }

    public void OnDrawGizmos()
    {
        foreach (var waypoint in _waypointsQueue)
        {
            Gizmos.DrawCube(waypoint.position, Vector3.one);
        }
    }

    public void EnqueueWaypoints(Transform[] waypoints)
    {
        foreach (var waypoint in waypoints)
        {
            _waypointsQueue.Enqueue(waypoint);
        }
    }

    public void FollowWaypoint()
    {
        if (_waypointsQueue.Count > 0)
        {
            Vector3 target = _waypointsQueue.Peek().transform.position;
            bool reachedCurrentWaypoint = _movement.MoveTowards(target, _completionDistance);
            if(reachedCurrentWaypoint)
            {
                _waypointsQueue.Dequeue();
            }
        }
    }
}

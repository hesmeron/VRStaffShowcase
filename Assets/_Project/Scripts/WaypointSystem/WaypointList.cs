using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointList : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

    private void OnDrawGizmos()
    {
        foreach (var waypoint in _waypoints)
        {
            Gizmos.DrawSphere(waypoint.position, 0.2f);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] 
    private Waypoint _nextWaypoint;
    [SerializeField] 
    private float _radius = 2f;

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(Vector3.zero, _radius);
    }

    public Waypoint GetNextWaypoint()
    {
        return null;
    }
}

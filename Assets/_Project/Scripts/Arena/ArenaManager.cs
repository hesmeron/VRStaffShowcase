using System;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [SerializeField] 
    private ArenaBehaviour _currentArena;

    [SerializeField]
    private PlayerBehaviour _playerBehaviour;

    private void Start()
    {
        Transform[] waypoints = (_currentArena.WaypointList.Waypoints);
        _playerBehaviour.WaypointFollower.EnqueueWaypoints(waypoints);
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _completionDistance = 0.05f;

    public WaypointFollower WaypointFollower => _waypointFollower;

    private WaypointFollower _waypointFollower;

    private void OnDrawGizmos()
    {
        if (_waypointFollower != null)
        {
            _waypointFollower.OnDrawGizmos();
        }
    }

    private void Awake()
    {
        _waypointFollower = new WaypointFollower(transform, _speed, _completionDistance);
    }

    private void Update()
    {
        _waypointFollower.FollowWaypoint();
    }

    public void GetDamaged()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    
}

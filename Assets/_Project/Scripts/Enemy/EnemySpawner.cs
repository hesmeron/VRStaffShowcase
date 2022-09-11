using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySystem _system;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawSphere(Vector3.zero, 0.2f);
    }
    
    public EnemyBehaviour SpawnProjectile()
    {
        return  _system.SpawnRandomProjectileAtPosition(transform.position);
    }
}

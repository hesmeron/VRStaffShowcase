using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : Controller<EnemySystem>
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawSphere(Vector3.zero, 0.2f);
    }
    
    public EnemyBehaviour SpawnProjectile()
    {
       
        return  System.SpawnRandomProjectileAtPosition(transform.position);
    }
}

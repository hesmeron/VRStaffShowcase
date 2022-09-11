using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//should be universalised later on
public sealed class EnemyDamageReceiver
{
    public event Action OnEnemyDestroyed;
    
    private int _splitsLeft;
    private Side _side;
    private EnemySystem _system;
    private Transform _transform;

    public EnemyDamageReceiver(Side side, int splitsAtStart, Transform transform, EnemySystem system)
    {
        _side = side;
        _splitsLeft = splitsAtStart;
        _transform = transform;
        _system = system;
    }
    
    /// <summary>
    /// Called when enemy is hit with the staff
    /// </summary>
    /// <param name="side">Side which hit the enemy</param>
    /// <param name="damageVector">A vector that staff traveled previous frame</param>
    public void ReceiveDamage(Side side, Vector3 damageVector)
    {
        if (side == _side)
        {
            if (_splitsLeft > 0)
            {
                SpawnLegacyProjectiles(damageVector);
            }
            OnEnemyDestroyed?.Invoke();
        }
    }
    
    /// <summary>
    /// Spawns projectiles after this one is destroyed
    /// </summary>
    /// <param name="damageVector">A vector that staff traveled previous frame</param>
    private void SpawnLegacyProjectiles(Vector3 damageVector)
    {
        Vector3 spawnVector = Vector3.Cross(damageVector, _transform.forward).normalized;
        float distance = damageVector.magnitude / Time.deltaTime;
        distance /= 20f;
        spawnVector *= Mathf.Clamp(distance, 0.2f, 1f);
        _system.SpawnOppositeSideProjectile(_side, _transform.position + spawnVector, _splitsLeft);
        _system.SpawnOppositeSideProjectile(_side, _transform.position - spawnVector, _splitsLeft);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//Both spawn manager and spawner may be later univerrsilised as generic classes
public class EnemySpawnManager
{
    private readonly TimeProbabilityEventHandler _timeProbabilityEventHandler;
    private readonly EnemySpawner[] _spawners;
    private float _spawnsLeft;
    private int _aliveEnemiesCount = 0;

    public EnemySpawnManager(Vector2 spawnRange, EnemySpawner[] spawners, 
                            TimeProbabilityEventData timeProbabilityEventData)
    {
        _spawnsLeft = Random.Range(spawnRange.x, spawnRange.y);
        _timeProbabilityEventHandler = new TimeProbabilityEventHandler(timeProbabilityEventData);
        _spawners = spawners;
    }
    
    public IEnumerator HandleEnemySpawning(Action onEncounterFinished)
    {
        while (_spawnsLeft > 0)
        {
            if (_timeProbabilityEventHandler.ProgressTimer() || _aliveEnemiesCount == 0)
            {
                _timeProbabilityEventHandler.ResetProbabilityEvent();
                _spawnsLeft--;
                EnemyBehaviour newEnemy = _spawners.RandomElement().SpawnProjectile();
                _aliveEnemiesCount++;
                newEnemy.EnemyDamageReceiver.OnEnemyDestroyed  += OnEnemyDestroyed;
            }
            yield return null;
        }

        while (_aliveEnemiesCount > 0)
        {
            yield return null;
        }
        onEncounterFinished.Invoke();
    }

    private void OnEnemyDestroyed()
    {
        _aliveEnemiesCount--;
    }
}
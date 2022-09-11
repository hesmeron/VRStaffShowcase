using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArenaBehaviour : MonoBehaviour
{
    [SerializeField]
    [Required] 
    [ChildGameObjectsOnly]
    private WaypointList _waypointList;
    [SerializeField] 
    private Vector2 _spawnRange;

    [SerializeField] 
    private EnemySpawner[] _enemySpawners;
    [SerializeField]
    private TimeProbabilityEventData _timeProbabilityEventHandlerData;

    private TimeProbabilityEventHandler _timeProbabilityEventHandler;
    private float _spawnLimit;
    private int _aliveEnemiesCount = 0;

    void Start()
    {
        _spawnLimit = Random.Range(_spawnRange.x, _spawnRange.y);
        _timeProbabilityEventHandler = new TimeProbabilityEventHandler(_timeProbabilityEventHandlerData);
    }

    private void Update()
    {
        if (_spawnLimit > 0)
        {
            HandleEnemySpawning();
        }
        else if (_aliveEnemiesCount == 0)
        {
            Debug.Log("ProgressToNextStage");
            gameObject.SetActive(false);
        }

    }

    public void HandleEnemySpawning()
    {
        if (_timeProbabilityEventHandler.ProgressTimer() || _aliveEnemiesCount == 0)
        {
            _timeProbabilityEventHandler.ResetProbabilityEvent();
            _spawnLimit--;
            EnemyBehaviour newEnemy = _enemySpawners.RandomElement().SpawnProjectile();
            _aliveEnemiesCount++;
            newEnemy.OnEnemyDestroyed  += OnEnemyDestroyed;
        }
    }

    private void OnEnemyDestroyed()
    {
        _aliveEnemiesCount--;
    }
}

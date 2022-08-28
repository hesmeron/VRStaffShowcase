using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileSpawner : Controller<EnemySystem>
{
    [SerializeField] private AnimationCurve _probabilityCurve  = AnimationCurve.Linear(0,0, 1,1);
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;
    private float _timePassed = 0f;
    private float _randomizedIndicator = 0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawSphere(Vector3.zero, 0.2f);
    }

    private void Start()
    {
        ResetProbabilityEvent();
    }

    private void Update()
    {
        if (ShouldSpawn())
        {
             ResetProbabilityEvent();
             SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        System.SpawnRandomProjectileAtPosition(transform.position);
    }

    private void ResetProbabilityEvent()
    {
        _randomizedIndicator = Random.Range(0, 1f);
        _timePassed = 0f;
    }

    private bool ShouldSpawn()
    {
        _timePassed += Time.deltaTime;
        float t = Mathf.Clamp01((_timePassed - _minSpawnTime) / _maxSpawnTime);
        float value = _probabilityCurve.Evaluate(t);
        return _randomizedIndicator <= value && _timePassed >= _minSpawnTime;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to create <see cref="TimeProbabilityEventHandler"/>
/// </summary>
[System.Serializable]
public struct TimeProbabilityEventData
{
    [SerializeField] private AnimationCurve _probabilityCurve;
    [SerializeField] private float _maxSpawnTime;
    [SerializeField] private float _minSpawnTime;

    public AnimationCurve ProbabilityCurve => _probabilityCurve;
    public float MaxSpawnTime => _maxSpawnTime;
    public float MinSpawnTime => _minSpawnTime;
}

/// <summary>
/// Handles events that are suposed to happen within some time range with varying probability 
/// </summary>
public class TimeProbabilityEventHandler
{
    private readonly AnimationCurve _probabilityCurve;
    private readonly float _minSpawnTime;
    private readonly float _maxSpawnTime;
    private float _randomizedIndicator = 0f;
    private float _timePassed = 0f;

    public TimeProbabilityEventHandler(TimeProbabilityEventData data)
    {
        _probabilityCurve = data.ProbabilityCurve;
        _minSpawnTime = data.MinSpawnTime;
        _maxSpawnTime = data.MaxSpawnTime;
        ResetProbabilityEvent();
    }
    public void ResetProbabilityEvent()
    {
        _randomizedIndicator = Random.Range(0, 1f);
        _timePassed = 0f;
    }

    public bool ProgressTimer()
    {
        _timePassed += Time.deltaTime;
        float t = Mathf.Clamp01((_timePassed - _minSpawnTime) / _maxSpawnTime);
        float value = _probabilityCurve.Evaluate(t);
        return _randomizedIndicator <= value && _timePassed >= _minSpawnTime;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedTimeEventHandler
{
    public event Action<float> OnCountdownChanged;
    public event Action OnCountdownCompleted;
    private float _timePassed;
    private readonly float _completionTime;

    public FixedTimeEventHandler(float completionTime)
    {
        _completionTime = completionTime;
    }

    public void ResetCountdown()
    {
        _timePassed = 0f;
    }
    public void ProgressTimer()
    {
        _timePassed += Time.deltaTime;
        float timePassedPercentage = _timePassed / _completionTime;
        if (timePassedPercentage >= 1)
        {
            OnCountdownCompleted?.Invoke();
        }
        else
        {
            OnCountdownChanged?.Invoke(timePassedPercentage);
        }
    }
}

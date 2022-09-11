using System;
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
        ResetCountdown();
    }

    public void ResetCountdown()
    {
        _timePassed = 0f;
    }
    public void ProgressTimer()
    {
        _timePassed += Time.deltaTime;
        float timePassedPercentage = Mathf.Clamp01(_timePassed / _completionTime);
        OnCountdownChanged?.Invoke(timePassedPercentage);
        if (timePassedPercentage >= 1)
        {
            OnCountdownCompleted?.Invoke();
        }
    }
}

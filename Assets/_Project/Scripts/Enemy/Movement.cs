using UnityEngine;
using Time = UnityEngine.Time;

[System.Serializable]
public struct MovementData
{
    public float Speed;
    public Transform Transform;

    public MovementData (Transform transform, float speed)
    {
        Speed = speed;
        Transform = transform;
    }
}
[System.Serializable]
public class Movement
{
    private float _speed;
    private Transform _transform;

    public Movement(MovementData data)
    {
        _speed = data.Speed;
        _transform = data.Transform;
    }
    public bool MoveTowards(Vector3 targetPosition, float stayDistance = 0f)
    {
        Vector3 direction = targetPosition - _transform.position;
        RotateTowards(direction);
        float distance = Mathf.Min(Time.deltaTime * _speed, direction.magnitude - stayDistance);
        _transform.position += _transform.forward * distance;
        return direction.magnitude - distance <= stayDistance;
    }

    private void RotateTowards(Vector3 direction)
    {
        _transform.forward = Utils.LerpVector(_transform.forward, direction, Time.deltaTime);
    }
}

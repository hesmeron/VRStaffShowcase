using UnityEngine;

[System.Serializable]
public class Movement
{
    [SerializeField] 
    private float _speed;
    private Transform _transform;

    public void Initialize(Transform transform)
    {
        _transform = transform;
    }
    public void Initialize(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;
    }
    public void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - _transform.position;
        RotateTowards(direction);
        float distance = Mathf.Min(Time.deltaTime * _speed, direction.magnitude);
        _transform.position += _transform.forward * distance;
    }

    private void RotateTowards(Vector3 direction)
    {
        _transform.forward = Utils.LerpVector(_transform.forward, direction, Time.deltaTime);
    }
}

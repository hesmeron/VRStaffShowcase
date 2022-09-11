using System;
using Sirenix.OdinInspector;
using UnityEngine;

public enum Side
{
    Left, 
    Right
}
[System.Serializable]
public class StaffSide
{
    [SerializeField, Required]
    private Transform _pivot;
    [SerializeField, Required]
    private LineRenderer _renderer;
    [SerializeField]
    private Side _side;

    private Vector3 _previousPosition;

    public Vector3 Position => _pivot.position;

    public void RenderSide(Vector3 armVector)
    {

        Vector3 end = HandleRaycast(armVector);
        _renderer.SetStartAndEnd(Position, end);
        _previousPosition = end;
    }

    private void TryDamageSurrounding(RaycastHit hit)
    {
        if (hit.transform.gameObject.TryGetComponent(out EnemyBehaviour destructable))
        {
            destructable.EnemyDamageReceiver.ReceiveDamage(_side, hit.point - _previousPosition);
            _previousPosition = hit.point;
        }
    }

    private Vector3 HandleRaycast(Vector3 armVector)
    {
        Ray ray = new Ray(Position, armVector);
        if (Physics.Raycast(ray,  out RaycastHit hit, armVector.magnitude))
        {
            TryDamageSurrounding(hit);
            return hit.point;
        }
        return Position + armVector;
    }
}
public class StaffBehaviour : MonoBehaviour
{
    [SerializeField] private float _staffLength = 2.3f;
    [SerializeField] [Required] private StaffSide _left;
    [SerializeField] [Required] private StaffSide _right;
    [SerializeField] [Required] private LineRenderer _middleRenderer;

    void Update()
    {
        Vector3 left = _left.Position;
        Vector3 right = _right.Position;
        Vector3 direction = right - left;
        float sideLengthLeft = (_staffLength - direction.magnitude) / 2;
        Vector3 armVector = (direction.normalized * sideLengthLeft);
        _left.RenderSide(-armVector);
        _right.RenderSide(armVector);
        _middleRenderer.SetStartAndEnd(right, left);
    }
}

using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
class StaffSide
{
    private Transform _pivot;
    private LineRenderer _renderer;

    public Vector3 Position => _pivot.position;

    public void RenderSide(Vector3 armVector)
    {
        
        Ray ray = new Ray(Position, armVector);
        Vector3 end;
        if (Physics.Raycast(ray,  out RaycastHit hit, armVector.magnitude))
        {
            end = hit.point;
        }
        else
        {
            end = Position + armVector;
        }
        _renderer.SetStartAndEnd(Position, end);
    }
}
public class Staff : MonoBehaviour
{
    [SerializeField] private float _staffLength = 2.3f;
    [SerializeField] [Required] private Transform _leftHandPivot;
    [SerializeField] [Required] private Transform _rightHandPivot;
    [SerializeField] [Required] private LineRenderer _leftRenderer;
    [SerializeField] [Required] private LineRenderer _middleRenderer;
    [SerializeField] [Required] private LineRenderer _rightRenderer;

    void Update()
    {
        Vector3 left = _leftHandPivot.position;
        Vector3 right = _rightHandPivot.position;
        Vector3 direction = right - left;
        float sideLengthLeft = (_staffLength - direction.magnitude) / 2;
        Vector3 armVector = (direction.normalized * sideLengthLeft);
        RenderSide(_leftRenderer, left, -armVector);
        RenderSide(_rightRenderer, right, armVector);
        _middleRenderer.SetStartAndEnd(right, left);
    }

    void RenderSide(LineRenderer renderer, Vector3 origin, Vector3 armVector)
    {
        Ray ray = new Ray(origin, armVector);
        Vector3 end;
        if (Physics.Raycast(ray,  out RaycastHit hit, armVector.magnitude))
        {
            end = hit.point;
        }
        else
        {
            end = origin + armVector;
        }
        renderer.SetStartAndEnd(origin, end);
    }
}

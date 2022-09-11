using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "GameplayConfigs/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] 
    private Material _leftMaterial;
    [SerializeField] 
    private Material _rightMaterial;
    [SerializeField] 
    private float _stayDistance;
    [SerializeField]
    private int _splitsLeft = 0;
    [SerializeField] 
    private float _timeUntilDamage= 3f;
    [SerializeField] 
    private float _speed = 3f;
    [SerializeField] 
    private Color _damagingEmissionColor;

    public string PropertyName => _propertyName;

    [SerializeField] 
    private string _propertyName;

    public float StayDistance => _stayDistance;

    public int SplitsLeft => _splitsLeft;

    public float TimeUntilDamage => _timeUntilDamage;

    public float Speed => _speed;

    public Color DamagingEmissionColor => _damagingEmissionColor;

    public Material GetMaterial(Side side)
    {
        Material material;
        switch (side)
        {
            case Side.Left:
                material = _leftMaterial;
                break;
            case Side.Right:
                material = _rightMaterial;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(side), side, null);
        }

        return material;
    }
}

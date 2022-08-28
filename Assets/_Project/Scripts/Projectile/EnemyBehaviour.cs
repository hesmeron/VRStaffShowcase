using System;
using UnityEngine;

public class EnemyBehaviour : Controller<EnemySystem>
{
    [SerializeField] 
    private Material _leftMaterial;
    [SerializeField] 
    private Material _rightMaterial;

    [SerializeField] 
    private Side _side;
    [SerializeField] 
    private MeshRenderer _renderer;
    [SerializeField] 
    private Movement _movement;
    [SerializeField] 
    private float _stayDistance;
    [SerializeField]
    private int _splitsLeft = 0;
    [SerializeField] 
    private float _timeUntilDamage= 3f;

    private PlayerBehaviour _player;
    private MaterialController _materialController;
    private float _timePassed = 0f;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");


    public void Initialize(EnemySystem system, Side side, GameMediator mediator, int splitsLeft)
    {
        SetMaterial(side);
        _splitsLeft = splitsLeft;
        _movement.Initialize(transform);
        base.Initialize(system);
        _player = mediator.Player;
    }

    private void OnEnable()
    {
        _timePassed = 0f;
    }

    private void Update()
    {
        Vector3 target = _player.transform.position;
        float distance = Vector3.Distance(target, transform.position);
        if (distance > _stayDistance)
        {
            _movement.MoveTowards(target);
        }
        else
        {
            TryDamage();
        }
    }

    private void TryDamage()
    {
        _timePassed += Time.deltaTime;
        float timePassedPercentage = _timePassed / _timeUntilDamage;
        if (timePassedPercentage >= 1)
        {
            _player.GetDamaged();
        }
        else
        {
            VisualizeDamagingProjectile(timePassedPercentage);
        }
    }

    private void VisualizeDamagingProjectile(float timePassedPercentage)
    {
        Color damagingColor = new Color(0.2f, 0.2f, 9f);
        Color color = Utils.LerpColor(Color.black, damagingColor, timePassedPercentage);
        _materialController.Material.SetColor(EmissionColor, color);
    }

    private void SetMaterial(Side side)
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

        _materialController = new MaterialController(_renderer, material);
    }

    /// <summary>
    /// Called when enemy is hit with the staff
    /// </summary>
    /// <param name="side">Side which hit the enemy</param>
    /// <param name="damageVector">A vector that staff traveled previous frame</param>
    public void DealDamage(Side side, Vector3 damageVector)
    {
        if (side == _side)
        {
            if (_splitsLeft > 0)
            {
                //SpawnLegacyProjectiles(damageVector);
            }
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// Spawns projectiles after this one is destroyed
    /// </summary>
    /// <param name="damageVector">A vector that staff traveled previous frame</param>
    private void SpawnLegacyProjectiles(Vector3 damageVector)
    {
        Vector3 spawnVector = Vector3.Cross(damageVector, transform.forward).normalized;
        float distance = damageVector.magnitude / Time.deltaTime;
        distance /= 20f;
        spawnVector *= Mathf.Clamp(distance, 0.2f, 1f);
        System.SpawnOppositeSideProjectile(_side, transform.position + spawnVector, _splitsLeft);
        System.SpawnOppositeSideProjectile(_side, transform.position - spawnVector, _splitsLeft);
    }
}

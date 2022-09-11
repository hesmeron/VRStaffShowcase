using System;
using UnityEngine;

public class EnemyBehaviour : Controller<EnemySystem>
{
    public event Action OnEnemyDestroyed;

    [SerializeField] 
    private EnemyConfig _config;
    [SerializeField]
    private MeshRenderer _renderer;
    private Side _side;
    private Movement _movement;
    private PlayerBehaviour _player;
    private MaterialController _materialController;
    private float _timePassed;
    private int _splitsLeft;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");


    public void Initialize(EnemySystem system, Side side, GameMediator mediator)
    {
        _side = side;
        _splitsLeft = _config.SplitsLeft;
        _materialController = new MaterialController(_renderer,_config.GetMaterial(side));
        MovementData movementData = new MovementData(transform, _config.Speed);
        _movement = new Movement(movementData);
        base.Initialize(system);
        _player = mediator.Player;
    }

    private void OnEnable()
    {
        _timePassed = 0f;
    }

    private void Update()
    {
        Vector3 target = _player.transform.position + (2f*Vector3.up);

        if(_movement.MoveTowards(target, _config.StayDistance))
        {
            TryDamage();
        }
    }

    private void TryDamage()
    {
        _timePassed += Time.deltaTime;
        float timePassedPercentage = _timePassed / _config.TimeUntilDamage;
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
    /// <summary>
    /// Called when enemy is hit with the staff
    /// </summary>
    /// <param name="side">Side which hit the enemy</param>
    /// <param name="damageVector">A vector that staff traveled previous frame</param>
    public void ReceiveDamage(Side side, Vector3 damageVector)
    {
        if (side == _side)
        {
            if (_splitsLeft > 0)
            {
                SpawnLegacyProjectiles(damageVector);
            }
            OnEnemyDestroyed?.Invoke();
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

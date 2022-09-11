using System;
using UnityEngine;

public class EnemyBehaviour : Controller<EnemySystem>
{
#region References
    [SerializeField] 
    private EnemyConfig _config;
    [SerializeField]
    private MeshRenderer _renderer;
#endregion

#region Components
    private Movement _movement;
    private PlayerBehaviour _player;
    private FixedTimeEventHandler _damageDealtTimer;
    private ApperanceController _appearanceController;
    private EnemyDamageReceiver _enemyDamageReceiver;
#endregion

    public EnemyDamageReceiver EnemyDamageReceiver => _enemyDamageReceiver;


    public void Initialize(EnemySystem system, Side side, GameMediator mediator)
    {
        _enemyDamageReceiver = new EnemyDamageReceiver(side, _config.SplitsLeft, transform, system);
        _enemyDamageReceiver.OnEnemyDestroyed += OnEnemyDestroyed;
        
        Material material = _renderer.CloneAndAssignMaterial();
        _appearanceController = new ApperanceController(material,
                                             Color.black, 
                                              _config.DamagingEmissionColor);
        
        _damageDealtTimer = new FixedTimeEventHandler(_config.TimeUntilDamage);
        _damageDealtTimer.OnCountdownChanged += _appearanceController.Visualize;
        _damageDealtTimer.OnCountdownCompleted += OnDamageCountdownCompleted;
        
        MovementData movementData = new MovementData(transform, _config.Speed);
        _movement = new Movement(movementData);
        
        _player = mediator.Player;
        base.Initialize(system);
    }

    private void OnEnemyDestroyed()
    {
        Destroy(this.gameObject);
    }

    private void OnDamageCountdownCompleted()
    {
        _player.GetDamaged();
    }
    
    private void OnEnable()
    {
        _damageDealtTimer.ResetCountdown();
    }

    private void Update()
    {
        Vector3 target = _player.transform.position + (2f*Vector3.up);

        if(_movement.MoveTowards(target, _config.StayDistance))
        {
            _damageDealtTimer.ProgressTimer();
        }
    }
}

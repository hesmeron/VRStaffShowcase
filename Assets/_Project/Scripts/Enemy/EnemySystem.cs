using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] 
    private GameMediator _gameMediator;
    [SerializeField]
    private EnemyBehaviour _enemyBehaviourPrefab;

    public void SpawnOppositeSideProjectile(Side side, Vector3 position, int splitsLeft)
    {
        splitsLeft--;
        switch (side)
        {
            case Side.Left:
                side = Side.Right;
                break;
            case Side.Right:
                side = Side.Left;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(side), side, null);
        }
        SpawnProjectileAtPosition(side, position, splitsLeft);
    }

    public EnemyBehaviour SpawnRandomProjectileAtPosition(Vector3 position)
    {
        return  SpawnProjectileAtPosition(Utils.RandomEnumValue<Side>(), position, 3);
    }
    //TO DO: Replace instantiation with object pooling
    public EnemyBehaviour SpawnProjectileAtPosition(Side side, Vector3 position, int splitsLeft)
    {
        EnemyBehaviour enemyBehaviour = Instantiate(_enemyBehaviourPrefab);
        enemyBehaviour.transform.position = position;
        enemyBehaviour.Initialize(this, side, _gameMediator);
        return enemyBehaviour;
    }


}

using System;
using UnityEngine;
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

    public void SpawnRandomProjectileAtPosition(Vector3 position)
    {
        SpawnProjectileAtPosition(Utils.RandomEnumValue<Side>(), position, 3);
    }
    public void SpawnProjectileAtPosition(Side side, Vector3 position, int splitsLeft)
    {
        EnemyBehaviour enemyBehaviour = Instantiate(_enemyBehaviourPrefab);
        enemyBehaviour.transform.position = position;
        enemyBehaviour.Initialize(this, side, _gameMediator, splitsLeft);
    }


}

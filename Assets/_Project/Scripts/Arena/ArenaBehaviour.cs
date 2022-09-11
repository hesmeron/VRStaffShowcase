using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArenaBehaviour : MonoBehaviour
{
    [SerializeField]
    [Required] 
    [ChildGameObjectsOnly]
    private WaypointList _waypointList;
    [SerializeField] 
    private Vector2 _spawnRange;

    [SerializeField] 
    private TimeProbabilityEventData _timeProbabilityEventData;

    [SerializeField]
    private EnemySpawner[] _enemySpawners;

    private EnemySpawnManager _enemySpawnManager;

    public WaypointList WaypointList => _waypointList;

    void Start()
    {
        _enemySpawnManager = new EnemySpawnManager(_spawnRange,
            _enemySpawners,
            _timeProbabilityEventData);
        StartCoroutine(_enemySpawnManager.HandleEnemySpawning(FinishArena));
    }

    private void FinishArena()
    {
        Debug.Log("ProgressToNextStage");
        gameObject.SetActive(false);
    }
}

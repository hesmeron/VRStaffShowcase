using Sirenix.OdinInspector;
using UnityEngine;

public class GameMediator : MonoBehaviour
{

    [SerializeField] 
    [Required]
    private PlayerBehaviour _player;

    public PlayerBehaviour Player => _player;
}

using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Controller<T> : MonoBehaviour
{
    [SerializeField] [HideInPlayMode] private T _system;

    protected T System => _system;

    public void Initialize(T system)
    {
        _system = system;
    }

}
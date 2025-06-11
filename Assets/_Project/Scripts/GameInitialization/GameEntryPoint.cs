using UnityEngine;
using Zenject;

public class GameEntryPoint : MonoBehaviour
{   
    [Inject] private IPlayerFactory _playerFactory;
    [Inject] private ILevelLogicFactory _levelLogicFactory;

    private void Awake() 
    {
        _playerFactory.Create(Vector3.zero, Quaternion.identity);
        _levelLogicFactory.Create(Vector3.zero, Quaternion.identity);
    }
}

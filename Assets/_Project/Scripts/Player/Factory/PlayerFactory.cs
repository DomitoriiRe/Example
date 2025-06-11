using Controller;
using UnityEngine;
using Zenject;
 
public class PlayerFactory : IPlayerFactory
{
    private readonly GameObject _playerPrefab;
    private readonly DiContainer _container;

    [Inject] public ICameraSettings _cameraSettings;

    public PlayerFactory(GameObject playerPrefab, DiContainer container)
    {
        _playerPrefab = playerPrefab;
        _container = container;
    }

    public GameObject Create(Vector3 position, Quaternion rotation)
    {
        GameObject playerInstance = _container.InstantiatePrefab(_playerPrefab, position, rotation, null);
         
        _container.InjectGameObject(playerInstance);
         
        var characterController = playerInstance.GetComponent<CharacterController>();
        var locomotionInput = playerInstance.GetComponent<PlayerLocomotionInput>();
        var ikAnimation = playerInstance.GetComponent<IKAnimationForBoxes>();
        var boxPoint = playerInstance.transform.Find("BoxPoint");
        var pickUpBox = playerInstance.GetComponent<PickUpBox>();

        _container.Bind<CharacterController>().FromInstance(characterController).AsSingle();
        _container.Bind<PlayerLocomotionInput>().FromInstance(locomotionInput).AsSingle();
        _container.Bind<IKAnimationForBoxes>().FromInstance(ikAnimation).AsSingle();
        _container.Bind<Transform>().WithId("BoxPoint").FromInstance(boxPoint).AsSingle();
        _container.Bind<PickUpBox>().FromInstance(pickUpBox).AsSingle();

        _cameraSettings.SetTarget(playerInstance);
        _cameraSettings.ApplySettings();

        return playerInstance;
    }
} 
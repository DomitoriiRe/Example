using Cinemachine; 
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;
    [SerializeField] private ItemDataList _itemDataList;
    [SerializeField] private ItemTaskGeneratorBinding _itemTaskGeneratorBinding;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private PlayerMovementSettings _playerMovementSettings;
    [SerializeField] private PlayerAnimatorSettings _playerAnimatorSettings;
    [SerializeField] private GameObject _levelLogicPrefab;
    [SerializeField] private PlatformBasedJoystickToggle _platformBasedJoystickToggle;

    public override void InstallBindings()
    {
        BindInteraction();

        BindPlatformBasedJoystickToggle();

        BindCinemachineFreeLook();

        BindCameraSettings();

        BindPlayerFactory();

        BindPlayerMovementSettings();

        BindPlayerAnimatorSettings();

        BindItemDataList();

        BindItemTaskGeneratorViewModel();

        BindCamera(); 

        BindItemTaskGeneratorBinding();

        BindLevelLogicFactory(); 
    }

    private void BindInteraction() => Container.Bind<IInteraction>().To<Interaction>().AsSingle();
    private void BindPlatformBasedJoystickToggle() => Container.Bind<PlatformBasedJoystickToggle>().FromInstance(_platformBasedJoystickToggle).AsSingle();
    private void BindCinemachineFreeLook() => Container.Bind<CinemachineFreeLook>().FromInstance(_cinemachineFreeLook).AsSingle(); 
    private void BindCamera() => Container.Bind<Camera>().FromInstance(Camera.main).AsSingle();
    private void BindCameraSettings() => Container.Bind<ICameraSettings>().To<CameraSettings>().AsSingle().WithArguments(_cinemachineFreeLook);
    private void BindPlayerFactory() => Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle().WithArguments(_playerPrefab, Container);
    private void BindPlayerMovementSettings() => Container.Bind<PlayerMovementSettings>().FromInstance(_playerMovementSettings).AsSingle();
    private void BindPlayerAnimatorSettings() => Container.Bind<PlayerAnimatorSettings>().FromInstance(_playerAnimatorSettings).AsSingle();
    private void BindLevelLogicFactory() => Container.Bind<ILevelLogicFactory>().To<LevelLogicFactory>().AsSingle().WithArguments(_levelLogicPrefab, Container);
    private void BindItemDataList() => Container.Bind<ItemDataList>().FromInstance(_itemDataList).AsSingle(); 
    private void BindItemTaskGeneratorViewModel() => Container.Bind<IItemTaskGeneratorViewModel>().To<ItemTaskGeneratorViewModel>().AsSingle();
    private void BindItemTaskGeneratorBinding() => Container.Bind<ItemTaskGeneratorBinding>().FromInstance(_itemTaskGeneratorBinding).AsSingle(); 
}
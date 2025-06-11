using Controller;
using UnityEngine;
using Zenject;

public class PlayerMovementHandler : MonoBehaviour, IPlayerMovementHandler
{
    [Header("Components")] 
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerLocomotionInput _playerLocomotionInput;
    public PlayerMovementState LastMovementState { get; private set; } = PlayerMovementState.Idling;

    private Transform _mainCameraTransform; 
    private IPlayerState _playerState;
    private PlayerMovementSettings _playerMovementSettings; 
    private IHandleRotation _handleRotation;

    [Inject] public void Construct(Camera camera, IPlayerState playerState, PlayerMovementSettings playerMovementSettings, IHandleRotation handleRotation)
    {
        _mainCameraTransform = camera.transform; 
        _playerState = playerState;
        _playerMovementSettings = playerMovementSettings; 
        _handleRotation = handleRotation;
    } 
      
    public void PlayerMovement()
    {  
        float clampLateralMagnitude = _playerMovementSettings.RunSpeed;

        var movementInput = new Vector3(_playerLocomotionInput.MovementInput.x, 0f, _playerLocomotionInput.MovementInput.y).normalized;

        var adjustedDirection = Quaternion.AngleAxis(_mainCameraTransform.eulerAngles.y, Vector3.up) * movementInput;

        var adjustedMovement = adjustedDirection * (_playerMovementSettings.RunSpeed * Time.deltaTime);
          
        _characterController.Move(adjustedMovement);
        _handleRotation.Rotation(adjustedDirection, transform, _mainCameraTransform, _playerMovementSettings.RotationSmoothTime);
    }
    public void UpdateMovementState()
    {
        LastMovementState = _playerState.CurrentPlayerMovementState;

        bool isMoving = _playerLocomotionInput.MovementInput != Vector2.zero;
        PlayerMovementState newState;
        newState = isMoving ? PlayerMovementState.Running : PlayerMovementState.Idling;

        if (newState != LastMovementState)
        {
            _playerState.SetPlayerMovementState(newState);
            LastMovementState = newState;
        }
    }
}  
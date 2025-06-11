using UnityEngine; 

namespace Controller
{ 
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerLocomotionInput _playerLocomotionInput; 
        [SerializeField] private PlayerMovementHandler _playerMovementHandler;
        [SerializeField] private PlayerAnimation _playerAnimation;
        [SerializeField] private CameraLookHandler _cameraLookHandler;
         
        private void Update()
        {
            _playerMovementHandler.UpdateMovementState(); 
            _playerMovementHandler.PlayerMovement(); 
            _playerAnimation.UpdateAnimationState();
            _cameraLookHandler.HandleCameraLook();
        } 
    }
}
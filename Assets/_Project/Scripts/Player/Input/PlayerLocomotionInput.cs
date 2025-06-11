using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller
{
    public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions
    {
        public PlayerControls PlayerControls { get; private set; }
        public Vector2 MovementInput { get; private set; }
        public Vector2 Look { get; private set; }
         
        private void Awake() => PlayerControls = new PlayerControls();

        private void OnEnable()
        {
            PlayerControls.PlayerLocomotionMap.Enable();
            PlayerControls.PlayerLocomotionMap.SetCallbacks(this);
        }

        private void OnDisable()
        {
            if (PlayerControls != null)
            {
                PlayerControls.PlayerLocomotionMap.RemoveCallbacks(this);
                PlayerControls.PlayerLocomotionMap.Disable();
            }
        }

        private void OnDestroy()
        {
            if (PlayerControls != null)
            {
                PlayerControls.Dispose();
                PlayerControls = null;
            }
        }

        public void OnMovement(InputAction.CallbackContext context) => MovementInput = context.ReadValue<Vector2>();

        public void OnLook(InputAction.CallbackContext context) => Look = context.ReadValue<Vector2>(); 
    }
}

using UnityEngine;
using Zenject;

namespace Controller
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerLocomotionInput _playerLocomotionInput;
        private PlayerAnimatorSettings _playerAnimatorSettings; 
        private IPlayerState _playerState;
        
        private static readonly int _inputXHash = Animator.StringToHash("inputX");
        private static readonly int _inputYHash = Animator.StringToHash("inputY");
        private static readonly int _inputMagnitudeHash = Animator.StringToHash("inputMagnitude");
          
        private Vector2 _blendVelocity = Vector2.zero;
        private Vector3 _currentBlendInput = Vector3.zero;

        [Inject] public void Construct(IPlayerState playerState, PlayerAnimatorSettings playerAnimatorSettings)
        {
            _playerState = playerState;
            _playerAnimatorSettings = playerAnimatorSettings;
        }
          
        public void UpdateAnimationState()
        {
            bool isState = _playerState.InState(); 
             
            Vector2 inputTarget = _playerLocomotionInput.MovementInput.normalized;
             
            if (inputTarget.magnitude < _playerAnimatorSettings.InputChangeThreshold)
            {
                _currentBlendInput = Vector3.Lerp(_currentBlendInput, Vector3.zero, _playerAnimatorSettings.StopLerpFactor * Time.deltaTime);
            }
            else
            {
                _currentBlendInput.x = Mathf.SmoothDamp(_currentBlendInput.x, inputTarget.x, ref _blendVelocity.x, _playerAnimatorSettings.BlendSmoothTime);
                _currentBlendInput.y = Mathf.SmoothDamp(_currentBlendInput.y, inputTarget.y, ref _blendVelocity.y, _playerAnimatorSettings.BlendSmoothTime);
            }
             
            float magnitude = Mathf.Lerp(_animator.GetFloat(_inputMagnitudeHash),
                                         _currentBlendInput.magnitude * _playerAnimatorSettings.InputMagnitudeDampFactor, 
                                         Time.deltaTime * _playerAnimatorSettings.StopLerpFactor);
              
            _animator.SetFloat(_inputXHash, _currentBlendInput.x);
            _animator.SetFloat(_inputYHash, _currentBlendInput.y);
            _animator.SetFloat(_inputMagnitudeHash, magnitude);
        }
    } 
} 
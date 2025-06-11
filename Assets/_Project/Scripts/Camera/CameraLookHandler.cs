using Cinemachine;
using Controller;
using UnityEngine;
using Zenject;

public class CameraLookHandler : MonoBehaviour
{
    [SerializeField] private PlayerLocomotionInput _playerLocomotionInput;
      
    private CinemachineFreeLook _freeLookCamera; 
    private PlayerMovementSettings _settingsSens;

    [Inject] public void Construct(PlayerMovementSettings settingsSens, CinemachineFreeLook freeLookCamera)
    {
        _settingsSens = settingsSens;
        _freeLookCamera = freeLookCamera; 
    } 
     
    public void HandleCameraLook()
    {
        Vector2 lookInput = _playerLocomotionInput.Look;

        _freeLookCamera.m_XAxis.Value += lookInput.x * _settingsSens.LookSensitivity * Time.deltaTime;
    }  
}

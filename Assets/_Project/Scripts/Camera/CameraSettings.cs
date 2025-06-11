using Cinemachine;
using UnityEngine;

public class CameraSettings : ICameraSettings
{
    private readonly CinemachineFreeLook _cinemachineFreeLook;
    private GameObject _player;

    public CameraSettings(CinemachineFreeLook cinemachineFreeLook) => _cinemachineFreeLook = cinemachineFreeLook; 
    public void SetTarget(GameObject player) => _player = player; 

    public void ApplySettings()
    {
        if (_player == null) return;
        _cinemachineFreeLook.Follow = _player.transform;
        _cinemachineFreeLook.LookAt = _player.transform;
    }
} 
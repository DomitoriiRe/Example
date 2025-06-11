using UnityEngine;

[CreateAssetMenu(menuName = "SO_Game/PlayerMovementSettings")]
public class PlayerMovementSettings : ScriptableObject
{
    [Range(3f, 15f)]
    [SerializeField] private float _runSpeed = 6f;

    [Range(0f, 1f)]
    [SerializeField] private float _rotationSmoothTime = 0.2f;

    [Range(1f, 500f)]
    [SerializeField] private float _lookSensitivity = 2f;

    public float RunSpeed => _runSpeed;
    public float RotationSmoothTime => _rotationSmoothTime;
    public float LookSensitivity => _lookSensitivity;
}
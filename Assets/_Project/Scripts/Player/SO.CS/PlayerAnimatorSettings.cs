using UnityEngine;

[CreateAssetMenu(menuName = "SO_Game/PlayerAnimatorSettings")]
public class PlayerAnimatorSettings : ScriptableObject
{
    [Range(0f, 1f)]
    [Tooltip("C���������� ��� ��������� �����������")]
    [SerializeField] private float _blendSmoothTime = 0.2f;

    [Range(0f, 1f)]
    [Tooltip("����������� ����� ��������� �����")]
    [SerializeField] private float _inputChangeThreshold = 0.05f;

    [Range(0f, 2f)]
    [Tooltip("����������� ����������� inputMagnitude ��� �������� �������� ����� ����������")]
    [SerializeField] private  float _inputMagnitudeDampFactor = 0.9f;

    [Range(0f, 10f)]
    [Tooltip("�������� ��������� �������� ��� ���������")]
    [SerializeField] private float _stopLerpFactor = 5f;

    public float BlendSmoothTime => _blendSmoothTime;
    public float InputChangeThreshold => _inputChangeThreshold;
    public float InputMagnitudeDampFactor => _inputMagnitudeDampFactor;
    public float StopLerpFactor => _stopLerpFactor;
}
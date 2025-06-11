using UnityEngine;

[CreateAssetMenu(menuName = "SO_Game/GameInitializerSettings")]
public class GameInitializerSettings : ScriptableObject
{
    [Range(0f, 4f)]
    [Tooltip("����� ������ ����� �������� � �������� ��������� ��������")]
    [SerializeField] private float _minLoadTime = 0.5f;

    [Range(0f, 1f)]
    [Tooltip("����������� ����������")]
    [SerializeField] private float _fillSmoothTime = 0.2f;

    [Range(0f, 5f)]
    [Tooltip("�������� ����� ��������")]
    [SerializeField] private float _postLoadDelay = 1f;

    public float MinLoadTime => _minLoadTime;
    public float FillSmoothTime => _fillSmoothTime;
    public float PostLoadDelay => _postLoadDelay;
}

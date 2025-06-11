using UnityEngine;

[CreateAssetMenu(menuName = "SO_Game/GameInitializerSettings")]
public class GameInitializerSettings : ScriptableObject
{
    [Range(0f, 4f)]
    [Tooltip("Чтобы уидеть экран загрузки и анимацию прогресса загрузки")]
    [SerializeField] private float _minLoadTime = 0.5f;

    [Range(0f, 1f)]
    [Tooltip("Сглаживание заполнения")]
    [SerializeField] private float _fillSmoothTime = 0.2f;

    [Range(0f, 5f)]
    [Tooltip("задержка после загрузки")]
    [SerializeField] private float _postLoadDelay = 1f;

    public float MinLoadTime => _minLoadTime;
    public float FillSmoothTime => _fillSmoothTime;
    public float PostLoadDelay => _postLoadDelay;
}

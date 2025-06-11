using UnityEngine;
using Zenject;

public class LevelLogicFactory : ILevelLogicFactory
{
    private readonly GameObject _levelLogicPrefab;
    private readonly DiContainer _container;

    public LevelLogicFactory(GameObject levelLogicPrefab, DiContainer container)
    {
        _levelLogicPrefab = levelLogicPrefab;
        _container = container;
    }

    public GameObject Create(Vector3 position, Quaternion rotation)
    {
        GameObject levelLogicInstance = _container.InstantiatePrefab(_levelLogicPrefab, position, rotation, null);

        _container.InjectGameObject(levelLogicInstance);

        return levelLogicInstance;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiatorService : IPrefabInstantiatorService
{
    private IAssetRegistry _assetRegistry;
    public PrefabInstantiatorService(IAssetRegistry assetRegistry) => _assetRegistry = assetRegistry;
    public List<GameObject> InstantiateAllFromLoadedLabel(string label)
    {
        var spawned = new List<GameObject>();

        if (!_assetRegistry.LabelToKeys.TryGetValue(label, out var keys))
        {
            return spawned;
        }

        foreach (var key in keys)
        {
            if (_assetRegistry.LoadedHandles.TryGetValue(key, out var handle) && handle.Result is GameObject prefab)
            {
                var instance = Object.Instantiate(prefab);
                spawned.Add(instance);
            }
        }

        return spawned;
    }
}

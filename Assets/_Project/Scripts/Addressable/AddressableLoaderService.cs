using Cysharp.Threading.Tasks; 
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations; 

public class AddressableLoaderService : IAddressableLoaderService
{ 
    public HashSet<string> LoadedLabels => _loadedLabels;
    private readonly HashSet<string> _loadedLabels = new();

    private IAssetRegistry _assetRegistry;
    public AddressableLoaderService(IAssetRegistry assetRegistry) => _assetRegistry = assetRegistry; 
     
    public async UniTask LoadByLabel(string label)
    {
        if (_loadedLabels.Contains(label))
            return;

        var locationsHandle = Addressables.LoadResourceLocationsAsync(label);
        await locationsHandle.Task;

        if (locationsHandle.Status != AsyncOperationStatus.Succeeded || locationsHandle.Result == null)
        {
            Addressables.Release(locationsHandle);
            return;
        }

        await LoadAssets(locationsHandle.Result, label);
        _loadedLabels.Add(label);

        Addressables.Release(locationsHandle);
    }

    public async UniTask LoadByLabels(IEnumerable<string> labels)
    {
        var validLabels = labels.Distinct().Where(label => !_loadedLabels.Contains(label)).ToList();
        if (validLabels.Count == 0) return;

        var locationHandles = new Dictionary<string, AsyncOperationHandle<IList<IResourceLocation>>>();

        foreach (var label in validLabels)
        {
            var handle = Addressables.LoadResourceLocationsAsync(label);
            locationHandles[label] = handle;
        }

        await UniTask.WhenAll(locationHandles.Values.Select(h => h.ToUniTask()));

        foreach (var kvp in locationHandles)
        {
            var label = kvp.Key;
            var handle = kvp.Value;

            if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null)
            {
                await LoadAssets(handle.Result, label);
                _loadedLabels.Add(label);
            }

            Addressables.Release(handle);
        }
    }

    private async UniTask LoadAssets(IList<IResourceLocation> locations, string label)
    {
        var tasks = new List<UniTask>();

        if (!_assetRegistry.LabelToKeys.ContainsKey(label))
            _assetRegistry.LabelToKeys[label] = new List<string>();

        foreach (var location in locations)
        {
            var key = location.PrimaryKey;

            if (_assetRegistry.LoadedHandles.ContainsKey(key))
                continue;

            var handle = Addressables.LoadAssetAsync<UnityEngine.Object>(location);
            _assetRegistry.LoadedHandles[key] = handle;
            _assetRegistry.LabelToKeys[label].Add(key);

            _assetRegistry.RegisterAssetForLoading();

            tasks.Add(HandleLoad(handle, key));
        }

        await UniTask.WhenAll(tasks);
    }

    private async UniTask HandleLoad(AsyncOperationHandle<UnityEngine.Object> handle, string key)
    {
        try
        {
            var asset = await handle.Task;
            if (asset != null)
                _assetRegistry.IncrementLoadedCount();
            else
                Debug.LogWarning($"[AddressableLoader] Loaded asset is null for key: {key}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[AddressableLoader] Failed to load asset: {e.Message}");
        }
    } 
}
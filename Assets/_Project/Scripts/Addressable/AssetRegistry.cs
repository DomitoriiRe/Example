using System.Collections.Generic;
using UniRx;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetRegistry : IAssetRegistry
{
    public Dictionary<string, List<string>> LabelToKeys => _labelToKeys;
    private readonly Dictionary<string, List<string>> _labelToKeys = new();

    public IReadOnlyReactiveProperty<int> LoadedCount => _loadedCount;
    private readonly ReactiveProperty<int> _loadedCount = new();

    public int TotalToLoad => _totalToLoad;
    private int _totalToLoad;

    public Dictionary<string, AsyncOperationHandle<UnityEngine.Object>> LoadedHandles => _loadedHandles;
    private readonly Dictionary<string, AsyncOperationHandle<UnityEngine.Object>> _loadedHandles = new();

    public void MarkAssetUnloaded()
    {
        _loadedCount.Value--;
        _totalToLoad--;
    }

    public void ClearLoadStats()
    {
        _loadedCount.Value = 0;
        _totalToLoad = 0;
    }

    public void RegisterAssetForLoading()
    {
        _totalToLoad++;
    }
    public void IncrementLoadedCount()
    {
        _loadedCount.Value++;
    }
}

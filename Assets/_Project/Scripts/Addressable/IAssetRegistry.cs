using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public interface IAssetRegistry 
{
    IReadOnlyReactiveProperty<int> LoadedCount { get; }
    int TotalToLoad { get; }
    Dictionary<string, List<string>> LabelToKeys { get; }
    Dictionary<string, AsyncOperationHandle<Object>> LoadedHandles { get; }
    void MarkAssetUnloaded();
    void ClearLoadStats();
    void RegisterAssetForLoading();
    void IncrementLoadedCount();
}

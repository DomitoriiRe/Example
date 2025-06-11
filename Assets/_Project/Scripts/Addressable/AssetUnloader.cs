using UnityEngine.AddressableAssets;

public class AssetUnloader : IAssetUnloader
{
    private IAssetRegistry _assetRegistry;
    private IAddressableLoaderService _addressableLoaderService;
    public AssetUnloader(IAssetRegistry assetRegistry, IAddressableLoaderService addressableLoaderService)
    {
        _assetRegistry = assetRegistry;
        _addressableLoaderService = addressableLoaderService;
    }
    public void UnloadByLabel(string label)
    {
        if (!_addressableLoaderService.LoadedLabels.Contains(label)) return;

        if (_assetRegistry.LabelToKeys.TryGetValue(label, out var keys))
        {
            foreach (var key in keys)
            {
                if (_assetRegistry.LoadedHandles.TryGetValue(key, out var handle))
                {
                    Addressables.Release(handle);
                    _assetRegistry.LoadedHandles.Remove(key);
                    _assetRegistry.MarkAssetUnloaded();
                }
            }

            _assetRegistry.LabelToKeys.Remove(label);
        }

        _addressableLoaderService.LoadedLabels.Remove(label);
    }

    public void UnloadAll()
    {
        foreach (var handle in _assetRegistry.LoadedHandles.Values)
        {
            Addressables.Release(handle);
        }

        _assetRegistry.LoadedHandles.Clear();
        _assetRegistry.LabelToKeys.Clear();
        _addressableLoaderService.LoadedLabels.Clear();
        _assetRegistry.ClearLoadStats();
    }
}

using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UniRx; 
using Zenject;

public class GameInitializer : MonoBehaviour
{  
    [SerializeField] private Image _loadingBar;
    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private GameInitializerSettings _gameInitializerSettings;
    [SerializeField] private AddressableLabel[] label;

    [Inject] private IAddressableLoaderService _addressableLoaderService;
    [Inject] private IAssetRegistry _assetRegistry;
    [Inject] private IPrefabInstantiatorService _prefabInstantiatorService;

    private bool _isInitialized;
    public bool IsInitialized => _isInitialized;

    public async UniTask WaitUntilInitializedAsync()
    {
        while (!_isInitialized)
            await UniTask.Yield();
    }

    private async void Start()
    {
        var loader = _assetRegistry;
        _loadingCanvas?.SetActive(true);
        _loadingBar.fillAmount = 0f;
        float startTime = Time.time;

        loader.LoadedCount
            .Subscribe(loaded =>
            {
                if (_loadingBar != null && loader.TotalToLoad > 0)
                {
                    float progress = (float)loaded / loader.TotalToLoad;
                    _loadingBar.DOFillAmount(progress, _gameInitializerSettings.FillSmoothTime).SetEase(Ease.OutQuad);
                }
            })
            .AddTo(this);
         
        foreach (var singleLabel in label)
        {
            await _addressableLoaderService.LoadByLabel(singleLabel.ToString());
        }
         
        foreach (var singleLabel in label)
        {
            _prefabInstantiatorService.InstantiateAllFromLoadedLabel(singleLabel.ToString());
        }
         
        float elapsed = Time.time - startTime;
        if (elapsed < _gameInitializerSettings.MinLoadTime)
            await UniTask.Delay(System.TimeSpan.FromSeconds(_gameInitializerSettings.MinLoadTime - elapsed));

        if (_loadingBar.fillAmount < 0.99f)
        {
            _loadingBar.DOFillAmount(1f, _gameInitializerSettings.FillSmoothTime).SetEase(Ease.OutQuad);
            await UniTask.Delay(System.TimeSpan.FromSeconds(_gameInitializerSettings.FillSmoothTime));
        }
        _isInitialized = true;
        await UniTask.Delay(System.TimeSpan.FromSeconds(_gameInitializerSettings.PostLoadDelay));

        if (_loadingCanvas != null && _loadingCanvas.gameObject != null)
            _loadingCanvas.SetActive(false);

    }
}

using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using Zenject;

public class ItemTaskGeneratorBinding : MonoBehaviour, IItemTaskGeneratorBinding
{  
    [Header("View")]
    [SerializeField] private ItemTaskGeneratorView _viewWorldCanvas;
    [SerializeField] private ItemTaskGeneratorView _viewCanvas;

    private IItemTaskGeneratorViewModel _viewModel;

    [Inject] public void Construct(IItemTaskGeneratorViewModel viewModel) => _viewModel = viewModel;  

    private void Start()
    {
        _viewModel.CurrentTaskItems.ObserveCountChanged().Subscribe(_ => OnTaskItemsUpdated()).AddTo(this);

        GenerateNewTaskAsync();
    }

    public void GenerateNewTaskAsync() => _viewModel.GenerateNewTaskAsync(); 

    public void OnTaskItemsUpdated()
    {
        _viewWorldCanvas.ShowItems(_viewModel.CurrentTaskItems);
        _viewCanvas.ShowItems(_viewModel.CurrentTaskItems);
    } 
} 
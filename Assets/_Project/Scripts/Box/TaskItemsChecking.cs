using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TaskItemsChecking : MonoBehaviour
{
    [Header("Ссылки на данные")]
    private IItemTaskGeneratorViewModel _itemTaskGeneratorViewModel;
    private ItemTaskGeneratorBinding _itemTaskGeneratorBinding;
    private IShelfItemList _shelfItemList;

    [Header("Результаты сверки")]
    private int _correctCount;
    private int _totalRequired;
    private int _excessCount;
    private int _missingCount;

    [Header("UI Elements")]
    [SerializeField] private Canvas _canvasResult;
    [SerializeField] private Button _returnButton;
    [SerializeField] private TMP_Text _correctText; 
    [SerializeField] private TMP_Text _excessText;
    [SerializeField] private TMP_Text _missingText;

    private PlatformBasedJoystickToggle _platformBasedJoystickToggle;
    private IInteraction _interaction;
    private Collider _currentCollider;

    [Inject] public void Construct(IItemTaskGeneratorViewModel viewModel, ItemTaskGeneratorBinding itemTaskGeneratorBinding,
                                   IShelfItemList shelfItemList, IInteraction interaction, PlatformBasedJoystickToggle platformBasedJoystickToggle)
    {
        _itemTaskGeneratorViewModel = viewModel;
        _itemTaskGeneratorBinding = itemTaskGeneratorBinding;
        _shelfItemList = shelfItemList;
        _interaction = interaction;
        _platformBasedJoystickToggle = platformBasedJoystickToggle;
    }

    private void OnTriggerEnter(Collider other)
    {
        _platformBasedJoystickToggle.CheckingPlatformForButton(true);
        _currentCollider = other;
    } 

    private void OnTriggerExit(Collider other)
    {
        _platformBasedJoystickToggle.CheckingPlatformForButton(false);
        if (_currentCollider == other)
            _currentCollider = null;
    }

    private void Start()
    {
        if (_returnButton != null)
        {
            _returnButton.onClick.AddListener(ReturnDefaultValues);
        }

        _canvasResult.enabled = false;

        _interaction.OnInteract
            .Where(_ => _currentCollider != null)
            .Subscribe(_ =>
            {
                CheckItems();
            }).AddTo(this);
    } 
     
     private void OnDisable() => _returnButton.onClick.RemoveListener(ReturnDefaultValues); 
     
    public void CheckItems()
    {
        if (_itemTaskGeneratorViewModel == null || _shelfItemList == null)
        { 
            return;
        }

        List<ItemData> requiredItems = _itemTaskGeneratorViewModel.CurrentTaskItems.ToList();
        List<ShelfItem> collectedShelfItems = _shelfItemList.Items;

        // Группируем по ID
        var requiredGrouped = requiredItems
            .GroupBy(item => item.ID)
            .ToDictionary(g => g.Key, g => g.Count());

        var collectedGrouped = collectedShelfItems
            .GroupBy(item => item.ID)
            .ToDictionary(g => g.Key, g => g.Count());

        _correctCount = 0;

        foreach (var kv in requiredGrouped)
        {
            int id = kv.Key;
            int requiredCount = kv.Value;
            int collectedCount = collectedGrouped.ContainsKey(id) ? collectedGrouped[id] : 0;

            _correctCount += Mathf.Min(requiredCount, collectedCount);
        }

        _totalRequired = requiredItems.Count;
        _excessCount = Mathf.Max(0, collectedShelfItems.Count - _correctCount);
        _missingCount = Mathf.Max(0, _totalRequired - _correctCount);

        if (Application.platform != RuntimePlatform.Android)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
         
        _canvasResult.enabled = true;
        _correctText.text = $"Правильно: {_correctCount}"; 
        _excessText.text = $"Лишние: {_excessCount}";
        _missingText.text = $"Не хватает: {_missingCount}"; 
    }

    private void ReturnDefaultValues()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        _canvasResult.enabled = false;

        _shelfItemList.ClearList();

        _correctCount = 0; 
        _excessCount = 0;
        _missingCount = 0;

        _correctText.text = $"Правильно: {_correctCount}";
        _excessText.text = $"Лишние: {_excessCount}";
        _missingText.text = $"Не хватает: {_missingCount}";

        _itemTaskGeneratorBinding.GenerateNewTaskAsync();
        _itemTaskGeneratorBinding.OnTaskItemsUpdated(); 
    }     
}
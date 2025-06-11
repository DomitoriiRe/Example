using UnityEngine;
using DG.Tweening;
using Zenject;
using UniRx;

public class ShelfZoneTrigger : MonoBehaviour
{
    [SerializeField] private ShelfItemSpawned _shelfItemSpawned;

    [Header("Параметры анимации")]
    [SerializeField] private float _moveDuration = 0.05f;
     
    private Transform _boxPoint;
    private PlatformBasedJoystickToggle _platformBasedJoystickToggle;
    private IShelfItemList _shelfItemList;
    private IItemTaskGeneratorViewModel _taskViewModel;
    private PickUpBox _pickUpBox; 
    private IInteraction _interaction; 
    private Collider _currentCollider;

    [Inject] public void Construct([Inject(Id = "BoxPoint")] Transform boxPoint,
                                   IShelfItemList shelfItemList, IItemTaskGeneratorViewModel taskViewModel,
                                   PickUpBox pickUpBox, IInteraction interaction, PlatformBasedJoystickToggle platformBasedJoystickToggle)
    {
        _boxPoint = boxPoint;
        _shelfItemList = shelfItemList;
        _taskViewModel = taskViewModel;
        _pickUpBox = pickUpBox;
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
        _interaction.OnInteract
            .Where(_ => _currentCollider != null)
            .Subscribe(_ =>
            {
                TryTakeItem();
            }).AddTo(this);
    }

    private void TryTakeItem()
    {
        if (_pickUpBox.CurrentBox == null || _shelfItemList.Items.Count >= _taskViewModel.CurrentTaskItems.Count) return;

        var spawnedItems = _shelfItemSpawned.GetSpawnedItems();
        if (spawnedItems.Count == 0) return;

        ShelfItem item = spawnedItems[0];
        _shelfItemList.AddToList(item);
        _shelfItemSpawned.RemoveSpawnedItem(item);

        JumpToMovingTarget(item.transform, _boxPoint, _moveDuration, 2f, 1);
         
        if (_shelfItemList.Items.Count >= _taskViewModel.CurrentTaskItems.Count)
        {
            _pickUpBox.ReplaceWithDroppedBox();
            return;
        }
    }

    private void JumpToMovingTarget(Transform itemTransform, Transform targetTransform, float duration, float jumpPower, int numJumps)
    {
        Vector3 startPos = itemTransform.position;
        float elapsed = 0f;

        DOTween.To(() => elapsed, x => elapsed = x, duration, duration)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                float t = Mathf.Clamp01(elapsed / duration); 
                Vector3 currentTargetPos = targetTransform.position;
                Vector3 horizontalPos = Vector3.Lerp(startPos, currentTargetPos, t);
                 
                float jump = Mathf.Sin(t * Mathf.PI * numJumps) * jumpPower;

                itemTransform.position = new Vector3(horizontalPos.x, horizontalPos.y + jump, horizontalPos.z);
            })
            .OnComplete(() =>
            {
                itemTransform.gameObject.SetActive(false);
            });
    }

}
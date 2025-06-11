using UnityEngine;
using System.Collections.Generic;
using Zenject; 
using UniRx;
public class TriggerPickUpBox : MonoBehaviour
{
    [SerializeField] private TypeTrigger _typeTrigger;
    [SerializeField] private PickUpBox _pickUpBox;
    [SerializeField] private List<Transform> _dropPoints;

    private PlatformBasedJoystickToggle _platformBasedJoystickToggle;
    private IInteraction _interaction;  
    private Collider _currentCollider;

    private int _dropIndex = 0;

    [Inject] public void Construct(IInteraction interaction, PlatformBasedJoystickToggle platformBasedJoystickToggle)
    {
        _interaction = interaction;
        _platformBasedJoystickToggle = platformBasedJoystickToggle;
    } 

    private void OnTriggerEnter(Collider other)
    {
        _platformBasedJoystickToggle.CheckingPlatformForButton(true);
        _currentCollider = other;
        if (_pickUpBox == null)
            _pickUpBox = other.GetComponent<PickUpBox>();
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
                PickUpOrDrop();
            }).AddTo(this);
    }

    private void PickUpOrDrop() 
    {
        if (_typeTrigger == TypeTrigger.Take)
        {
            if (!_pickUpBox.IsHoldingBox)
                _pickUpBox.PickUp();
        }
        else if (_typeTrigger == TypeTrigger.Give && _pickUpBox.IsHoldingBox)
        {
            Transform droppedBox = _pickUpBox.Drop();
            if (droppedBox != null && _dropPoints.Count > 0)
            {
                int pointIndex = _dropIndex % _dropPoints.Count;
                Transform targetPoint = _dropPoints[pointIndex];

                droppedBox.position = targetPoint.position;
                droppedBox.rotation = targetPoint.rotation;

                _dropIndex++;
            }
        }
    } 
}
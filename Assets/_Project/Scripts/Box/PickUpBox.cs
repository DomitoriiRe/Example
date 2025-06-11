using UnityEngine; 

public class PickUpBox : MonoBehaviour
{
    [SerializeField] private Transform _boxPrefab;
    [SerializeField] private Transform _boxForDropPrefab; 
    [SerializeField] private Transform _holdPoint;  
    [SerializeField] private IKAnimationForBoxes _iKAnimationForBoxes; 

    public bool IsHoldingBox => _currentBox != null;

    private Transform _currentBox;
    public Transform CurrentBox => _currentBox;

    public void PickUp()
    {
        if (_currentBox != null) return; // Не поднимать, если уже есть коробка

        _currentBox = Instantiate(_boxPrefab, _holdPoint);
        _currentBox.localPosition = Vector3.zero;
        _currentBox.localRotation = Quaternion.identity;

        _iKAnimationForBoxes.IsHoldingBox = true;
    }

    public Transform Drop()
    {
        if (_currentBox == null) return null;
         
        Vector3 dropPosition = _currentBox.position;
        Quaternion dropRotation = _currentBox.rotation;

        Destroy(_currentBox.gameObject);
        _currentBox = null;
        _iKAnimationForBoxes.IsHoldingBox = false;

        // Спавним новую коробку (замена)
        if (_boxForDropPrefab != null) return Instantiate(_boxForDropPrefab, dropPosition, dropRotation);

        return null;
    }

    public void ReplaceWithDroppedBox()
    {
        if (_currentBox.gameObject == null) return;
        
        Destroy(_currentBox.gameObject);
        _currentBox = null;

        _currentBox = Instantiate(_boxForDropPrefab, _holdPoint);
        _currentBox.localPosition = Vector3.zero;
        _currentBox.localRotation = Quaternion.identity;
    }
}

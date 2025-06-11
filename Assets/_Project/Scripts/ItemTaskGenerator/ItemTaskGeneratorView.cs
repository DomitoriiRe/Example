using UnityEngine; 
using System.Collections.Generic;

public class ItemTaskGeneratorView : MonoBehaviour, IItemTaskGeneratorView
{
    [SerializeField] private Transform _contentParent;
    [SerializeField] private ItemView _itemViewPrefab;

    private readonly List<ItemView> _spawnedItems = new List<ItemView>();

    public void ShowItems(IReadOnlyList<ItemData> items)
    {
        ClearItems();

        foreach (var item in items)
        {
            var itemView = Instantiate(_itemViewPrefab, _contentParent);
            itemView.Setup(item);
            _spawnedItems.Add(itemView);
        }
    }

    private void ClearItems()
    {
        foreach (var itemView in _spawnedItems)
        {
            Destroy(itemView.gameObject);
        }
        _spawnedItems.Clear();
    }
}

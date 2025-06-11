using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShelfItemSpawned : MonoBehaviour
{
    [SerializeField] private int _shelfIndex; 
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();

    private List<ShelfItem> _spawnedItems = new List<ShelfItem>(); 
    private ItemDataList _itemsToSpawn;

    [Inject] public void Construct(ItemDataList itemsToSpawn) => _itemsToSpawn = itemsToSpawn; 

    private void Start() => SpawnItems(); 

    public void SpawnItems()
    {  
        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            ItemData itemData = _itemsToSpawn.Items[_shelfIndex];
            Transform spawnPoint = _spawnPoints[i];

            GameObject itemObject = Instantiate(itemData.ItemPrefab, spawnPoint.position, spawnPoint.rotation, transform);
            ShelfItem shelfItem = itemObject.GetComponent<ShelfItem>();

            if (shelfItem != null)
            {
                shelfItem.Setup(itemData.ID);
                AddItem(shelfItem);
            }
        }
    }

    public void AddItem(ShelfItem item) => _spawnedItems.Add(item); 

    public void RemoveSpawnedItem(ShelfItem item)
    {  
        if (_spawnedItems.Contains(item))
        {
            _spawnedItems.Remove(item);
        } 
    }

    public List<ShelfItem> GetSpawnedItems()
    {
        return _spawnedItems;
    }
}
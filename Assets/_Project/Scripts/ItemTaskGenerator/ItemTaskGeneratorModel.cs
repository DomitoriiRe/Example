using System.Collections.Generic;
using UnityEngine;

public class ItemTaskGeneratorModel : IItemTaskGeneratorModel
{
    private List<ItemData> _requiredItems;
    private int _itemCount;

    public IReadOnlyList<ItemData> RequiredItems => _requiredItems;

    public ItemTaskGeneratorModel(ItemDataList itemDatabase) => GenerateRandomTask(itemDatabase); 

    private void GenerateRandomTask(ItemDataList itemDatabase)
    {
        _requiredItems = new List<ItemData>();
        _itemCount = Random.Range(1, 7);

        int availableCount = itemDatabase.Items.Count;
        if (availableCount == 0)
        {
            Debug.LogWarning("Item database is empty.");
            return;
        }

        for (int i = 0; i < _itemCount; i++)
        {
            int randomIndex = Random.Range(0, availableCount);
            ItemData randomItem = itemDatabase.Items[randomIndex];
            _requiredItems.Add(randomItem);
        }
    }
} 
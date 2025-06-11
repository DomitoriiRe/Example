using UniRx; 

public class ItemTaskGeneratorViewModel : IItemTaskGeneratorViewModel
{
    public ReactiveCollection<ItemData> CurrentTaskItems { get; } = new ReactiveCollection<ItemData>();
    private ItemDataList _itemDatabase;
    private ItemTaskGeneratorModel _model;

    public ItemTaskGeneratorViewModel(ItemDataList itemDatabase) => _itemDatabase = itemDatabase; 

    public void GenerateNewTaskAsync()
    {   
        _model = new ItemTaskGeneratorModel(_itemDatabase);

        CurrentTaskItems.Clear();
        foreach (var item in _model.RequiredItems)
        {
            CurrentTaskItems.Add(item);
        }
    }
}

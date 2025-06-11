using UniRx; 

public interface IItemTaskGeneratorViewModel
{ 
    ReactiveCollection<ItemData> CurrentTaskItems { get; } 
    void GenerateNewTaskAsync();
}

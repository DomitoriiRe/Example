using System.Collections.Generic;

public interface IItemTaskGeneratorModel
{
    IReadOnlyList<ItemData> RequiredItems { get; }
}

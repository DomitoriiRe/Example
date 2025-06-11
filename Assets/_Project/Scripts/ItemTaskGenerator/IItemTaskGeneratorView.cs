using System.Collections.Generic;

public interface IItemTaskGeneratorView
{
    void ShowItems(IReadOnlyList<ItemData> items);
}

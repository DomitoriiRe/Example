using System.Collections.Generic;

public interface IShelfItemList 
{
    List<ShelfItem> Items { get; }
    void AddToList(ShelfItem item);
    void ClearList();
}

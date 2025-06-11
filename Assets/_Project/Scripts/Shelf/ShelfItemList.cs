using System.Collections.Generic; 

public class ShelfItemList : IShelfItemList
{
    private List<ShelfItem> _items = new List<ShelfItem>();
    public List<ShelfItem> Items => _items; 
    public void AddToList(ShelfItem item) => Items.Add(item); 
    public void ClearList() => Items.Clear(); 
} 
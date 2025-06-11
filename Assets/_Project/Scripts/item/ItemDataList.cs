using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO_Game/ItemData")]
public class ItemDataList : ScriptableObject
{
    public List<ItemData> Items;
}
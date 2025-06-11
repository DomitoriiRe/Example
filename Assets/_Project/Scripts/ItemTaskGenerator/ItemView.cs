using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _itemImage; 

    public void Setup(ItemData itemData) => _itemImage.sprite = itemData.Sprite;  
}

using UnityEngine;

public class ShelfItem : MonoBehaviour
{
    public int ID { get; private set; } 
    public void Setup(int id) => ID = id; 
}

using UnityEngine;

public interface IPlayerFactory
{
    GameObject Create(Vector3 position, Quaternion rotation);
} 
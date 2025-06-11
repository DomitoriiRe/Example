using UnityEngine;

public interface ILevelLogicFactory
{
    GameObject Create(Vector3 position, Quaternion rotation);
}

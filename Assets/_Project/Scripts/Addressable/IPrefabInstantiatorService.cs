using System.Collections.Generic;
using UnityEngine;

public interface IPrefabInstantiatorService
{
    List<GameObject> InstantiateAllFromLoadedLabel(string label);
}

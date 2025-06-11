using UnityEngine;

public interface IHandleRotation
{
    void Rotation(Vector3 adjustedDirection, Transform objectTransform, Transform cameraTransform, float rotationSmoothTime);
}

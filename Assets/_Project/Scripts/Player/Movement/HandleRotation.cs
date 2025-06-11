using UnityEngine;

public class HandleRotation : IHandleRotation
{
    private float _currentVelocity; 
     
    void IHandleRotation.Rotation(Vector3 adjustedDirection, Transform objectTransform, Transform cameraTransform, float rotationSmoothTime)
    {
        if (adjustedDirection.magnitude > 0)
        {
            adjustedDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(adjustedDirection);
            float newYRotation = Mathf.SmoothDampAngle(objectTransform.eulerAngles.y, targetRotation.eulerAngles.y, ref _currentVelocity, rotationSmoothTime);
            objectTransform.rotation = Quaternion.Euler(0, newYRotation, 0);
        }
    }
}

using UnityEngine;

public class PlatformBasedJoystickToggle : MonoBehaviour, IPlatformBasedJoystickToggle
{
    [SerializeField] private Canvas _gamepadCanvas;
    [SerializeField] private GameObject _gamepadButton;
    [SerializeField] private GameObject _pcButton;

    private void Start()
    {
        ChecingPlatformForCanvas();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ChecingPlatformForCanvas()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            _gamepadCanvas.enabled = true;
        }
        else
        {
            _gamepadCanvas.enabled = false;
        }
    }

    public void CheckingPlatformForButton(bool value)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (_gamepadButton != null)
                _gamepadButton.SetActive(value);
        }
        else
        {
            if (_pcButton != null)
                _pcButton.SetActive(value);
        }
    }

}

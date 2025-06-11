using System;
using UniRx; 
using UnityEngine.InputSystem;

public class Interaction : IInteraction, PlayerControls.IInteractionsActions
{
    private PlayerControls _playerControls;

    private Subject<Unit> _interactSubject = new Subject<Unit>();
    public IObservable<Unit> OnInteract => _interactSubject;

    public Interaction()
    {
        _playerControls = new PlayerControls();
        _playerControls.Interactions.SetCallbacks(this);
        _playerControls.Interactions.Enable();
    }

    public void Dispose()
    {
        _playerControls.Interactions.Disable();
        _playerControls.Dispose();
        _interactSubject.Dispose();
    }

    public void OnActionE(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _interactSubject.OnNext(Unit.Default);
        }
    }
}
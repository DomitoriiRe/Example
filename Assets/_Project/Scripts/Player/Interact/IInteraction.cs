using System;
using UniRx;

public interface IInteraction
{
    IObservable<Unit> OnInteract { get; }
}
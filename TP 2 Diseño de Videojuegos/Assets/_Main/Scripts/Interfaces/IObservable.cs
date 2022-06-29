using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable
{
    List<IObserver> Subscribers { get;}

    void Suscribe(IObserver observer);
    void Unsuscribe(IObserver observer);
    void NotifyAll(string message, params object[] args);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Weapons;

namespace Items
{
    public class EndgameInteraction : MonoBehaviour, IObservable
    {
        private List<IObserver> _subscribers = new List<IObserver>();
        public List<IObserver> Subscribers => _subscribers;
        
        private SoundManager _sound;

        private void Awake()
        {
            _sound = GetComponent<SoundManager>();
        }

        public void Suscribe(IObserver observer)
        {
            if (_subscribers.Contains(observer)) return;
            _subscribers.Add(observer);
        }

        public void Unsuscribe(IObserver observer)
        {
            if (!_subscribers.Contains(observer)) return;
            _subscribers.Remove(observer);
        }

        public void NotifyAll(string message, params object[] args)
        {
            if (_subscribers.Count < 1) return;

            foreach (var suscriber in _subscribers)
            {
                suscriber.OnNotify(message, args);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _sound.PlaySound("Win");
            GameManager.Instance.SetState(GameManager.GameStates.Win);
        }
    }
}
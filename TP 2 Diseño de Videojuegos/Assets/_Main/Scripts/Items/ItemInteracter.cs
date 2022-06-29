using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemInteracter : ItemUser, IObservable
    {
        private IInteractable _item;
        private List<IObserver> _subscribers = new List<IObserver>();
        public List<IObserver> Subscribers => _subscribers;
        private SoundManager _sound;

        private void Awake()
        {
            _sound = GetComponent<SoundManager>();
        }

        private bool IsItemInteractable()
        {
            if (!IsItemInRange) return false;

            if (!ItemInRange.ItemObject.TryGetComponent<IInteractable>(out var item)) return false;

            _item = item;
            return true;
        }

        private void CreateGameMessage(string message)
        {
            NotifyAll("MESSAGE", message);
        }

        public void InteractWithItem()
        {
            if (!IsItemInteractable())
            {
                return;
            }
            var message = (string)_item.Test(gameObject)[0];
            var isUsed = (bool)_item.Test(gameObject)[1];
            CreateGameMessage(message);

            if (isUsed)
                _sound.PlaySound("UseItem");
            else
                _sound.PlaySound("Negative");

            Debug.Log(isUsed);
            ItemInRange = null;
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
    }

}
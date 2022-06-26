using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemLooter : ItemUser, IObservable
    {
        private List<IObserver> _subscribers = new List<IObserver>();
        private List<IItem> _inventory = new List<IItem>();
        public List<IObserver> Subscribers => _subscribers;
        public List<IItem> Inventory => _inventory;


        private ILootable _item;

        private bool IsItemlootable()
        {
            if (!IsItemInRange) return false;

            if (!ItemInRange.ItemObject.TryGetComponent<ILootable>(out var item)) return false;

            _item = item;
            return true;
        }

        private string CreateGameMessage()
        {
            var itemName = _item.ItemName;
            var itemCount = 0;

            for (int i = 0; i < _inventory.Count; i++)
            {
                var currentItem = _inventory[i];
                if(currentItem.Data == _item.GetItem().Data)
                    itemCount++;
            }

            return $"{itemCount} {itemName} Collected";
        }

        public void LootItem()
        {
            if (!IsItemlootable()) return;

            _inventory.Add(_item.Loot());
            NotifyAll("MESSAGE", CreateGameMessage());
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

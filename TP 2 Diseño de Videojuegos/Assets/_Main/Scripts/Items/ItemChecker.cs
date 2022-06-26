using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemChecker : MonoBehaviour, IObservable
    {
        [SerializeField] private ItemTypeSO itemToCheck;
        [SerializeField] private int ammountNeeded;
        private List<IObserver> _subscribers = new List<IObserver>();
        public List<IObserver> Subscribers => _subscribers;
        //checks if the player has the item and the ammount needed

        private void CheckItems(List<IItem> itemList)
        {
            int ammontInInventory = 0;

            for (int i = 0; i < itemList.Count; i++)
            {
                var currentItem = itemList[i];
                if (currentItem.Data == itemToCheck)
                {
                    ammontInInventory++;
                    if (ammontInInventory >= ammountNeeded)
                    {
                        GameManager.Instance.SetState(GameManager.GameStates.Win);
                    }
                }                    
            }

            if (ammontInInventory < ammountNeeded)
                NotifyAll("MESSAGE", $"You Need {ammountNeeded - ammontInInventory} {itemToCheck.ItemName} To Finish The Reserach");
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
            var itemPickUp = other.GetComponent<ItemLooter>();

            if (itemPickUp == null) return;

            CheckItems(itemPickUp.Inventory);
        }
    }
}
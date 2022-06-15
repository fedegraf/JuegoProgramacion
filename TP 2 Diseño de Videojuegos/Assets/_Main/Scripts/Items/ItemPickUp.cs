using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemPickUp : MonoBehaviour, IItemPickUp
    {
        public bool CanPickUp { get; private set; }
        public List<IItem> ItemsInInventory => _itemsInInventory;

        private List<IItem> _itemsInInventory = new List<IItem>();
        private IItem _itemToPickUp;
        private bool _isItemInRange;

        private void Start()
        {
            SetCanPickUp(true);
        }

        public void SetCanPickUp(bool canPickUp)
        {
            CanPickUp = canPickUp;
        }

        public void PickUp()
        {
            if (!_isItemInRange) return;

            _itemsInInventory.Add(_itemToPickUp.PickUp());
        }

        public void UseItemInGround()
        {
            if (!_isItemInRange) return;

            if (_itemToPickUp.ItemObject.GetComponent<IUsableItem>().Use(gameObject))
            {
                Debug.Log($"{_itemToPickUp.Data.ItemName} Used");
            }
            else
                Debug.Log("You Cant Use That Item Right Now");
        }

        public void ShowItems()
        {
            if (_itemsInInventory.Count < 1)
            {
                Debug.Log("No Items In Bag");
                return;
            }

            for (int i = 0; i < _itemsInInventory.Count; i++)
            {
                Debug.Log($"Item: {_itemsInInventory[i].Data.ItemName}");
            }

            Debug.Log($"Item In Bag: {_itemsInInventory.Count}");
        }


        private void OnTriggerStay(Collider other)
        {
            var itemToPickUp = other.GetComponent<IItem>();

            if (itemToPickUp == null && !CanPickUp)
            {
                if (_itemToPickUp != null)
                {
                    _itemToPickUp = null;
                    _isItemInRange = false;
                }
                return;
            }
            else
            {
                _itemToPickUp = itemToPickUp;
                _isItemInRange = true;
            }
        }
    }

}
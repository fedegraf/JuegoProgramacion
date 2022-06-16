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
        private bool IsItemInRange()
        {
            return _itemToPickUp != null;
        }

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
            if (!IsItemInRange()) return;

            _itemsInInventory.Add(_itemToPickUp.PickUp());
            Debug.Log($"{_itemToPickUp.Data.ItemName} Added To Inventory");
            _itemToPickUp = null;
        }

        public void UseItemInGround()
        {
            if (!IsItemInRange()) return;

            if (_itemToPickUp.ItemObject.GetComponent<IUsableItem>().Use(gameObject))
            {
                Debug.Log($"{_itemToPickUp.Data.ItemName} Used");
                _itemToPickUp = null;
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
            if (other.TryGetComponent<IItem>(out var itemToPickUp) && CanPickUp)
            {
                _itemToPickUp = itemToPickUp;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<IItem>() != null)
            {
                _itemToPickUp = null;
            }
        }
    }

}
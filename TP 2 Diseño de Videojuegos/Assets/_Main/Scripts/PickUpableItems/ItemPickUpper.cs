using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class ItemPickUpper : MonoBehaviour, IPickUpper
    {
        private IPickUpable _itemToPickUp;
        private List<IPickUpable> _itemsList = new List<IPickUpable>();
        public List<IPickUpable> ItemsList => _itemsList;

        public void PickUpItem()
        {
            if (_itemToPickUp == null)
            {
                Debug.Log("No Item Nearby");
                return;
            }

            _itemsList.Add(_itemToPickUp.PickedUp());
        }

        public void ShowItems()
        {
            if (ItemsList.Count < 1)
            {
                Debug.Log("No Items");
                return;
            }

            for (int i = 0; i < ItemsList.Count; i++)
            {
                Debug.Log($"Item {i+1}: {ItemsList[i].ItemName},{ItemsList[i].ItemTyoe}");
            }
        }

        private void OnTriggerStay(Collider other)
        {
            var itemToGrab = other.GetComponent<IPickUpable>();
            if (itemToGrab == null) return;

            _itemToPickUp = itemToGrab;
        }

        private void OnTriggerExit(Collider other)
        {
            var itemToGrab = other.GetComponent<IPickUpable>();
            if (itemToGrab == null) return;

            _itemToPickUp = null;
        }
    }
}

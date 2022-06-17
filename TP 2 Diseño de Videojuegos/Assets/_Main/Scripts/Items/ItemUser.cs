using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemUser : MonoBehaviour
    {
        public IItem ItemInRange { get; protected set; }
        public bool IsItemInRange => ItemInRange != null;


        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<IItem>(out var itemToPickUp))
            {
                ItemInRange = itemToPickUp;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<IItem>() != null)
            {
                ItemInRange = null;
            }
        }
    }

}
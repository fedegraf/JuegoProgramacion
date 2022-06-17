using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemInteracter : ItemUser
    {
        private IInteractable _item;

        private bool IsItemInteractable()
        {
            if (!IsItemInRange) return false;

            if (!ItemInRange.ItemObject.TryGetComponent<IInteractable>(out var item)) return false;

            _item = item;
            return true;
        }

        public void InteractWithItem()
        {
            if (!IsItemInteractable()) return;

            _item.Interact(gameObject);
            ItemInRange = null;
        }
    }

}
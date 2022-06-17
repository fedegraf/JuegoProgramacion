using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemLooter : ItemUser
    {
        private List<IItem> _inventory = new List<IItem>();
        public List<IItem> Inventory => _inventory;

        private ILootable _item;

        private bool IsItemlootable()
        {
            if (!IsItemInRange) return false;

            if (!ItemInRange.ItemObject.TryGetComponent<ILootable>(out var item)) return false;

            _item = item;
            return true;
        }

        public void LootItem()
        {
            if (!IsItemlootable()) return;

            _inventory.Add(_item.Loot());
            ItemInRange = null;
        }
    }
}

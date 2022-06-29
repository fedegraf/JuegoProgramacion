using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class LootableItem : BaseItem, ILootable
    {
        public string ItemName => Data.ItemName;

        public IItem Loot()
        {
            Destroy(gameObject);
            return this;
        }

        public IItem GetItem()
        {
            return this;
        }
    }
}

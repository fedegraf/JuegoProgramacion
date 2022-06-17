using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class LootableItem : BaseItem, ILootable
    {
        public IItem Loot()
        {
            Destroy(gameObject);
            return this;
        }
    }
}

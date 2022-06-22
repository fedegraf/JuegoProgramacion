using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public interface ILootable
    {
        public string ItemName { get; }
        public IItem Loot();
        public IItem GetItem();
    }
}

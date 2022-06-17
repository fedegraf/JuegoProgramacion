using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public interface ILootable
    {
        public IItem Loot();
    }
}

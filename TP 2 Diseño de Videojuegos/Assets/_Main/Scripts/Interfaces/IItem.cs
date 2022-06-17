using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public interface IItem
    {
        public GameObject ItemObject { get; }
        public ItemTypeSO Data { get; }
        public void SetData(ItemTypeSO newData);
    }
}

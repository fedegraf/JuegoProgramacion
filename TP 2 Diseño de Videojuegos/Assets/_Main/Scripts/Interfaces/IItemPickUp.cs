using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public interface IItemPickUp
    {
        public bool CanPickUp { get; }
        public List<IItem> ItemsInInventory { get; }

        public void SetCanPickUp(bool canPickUp);
        public void PickUp();
        public void UseItemInGround();
        public void ShowItems();
    }
}

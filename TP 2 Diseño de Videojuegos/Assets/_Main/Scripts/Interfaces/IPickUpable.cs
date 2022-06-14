using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public interface IPickUpable
    {
        public string ItemName { get; }
        public ItemTyoe ItemTyoe { get; }
        public IPickUpable PickedUp();
    }

    public enum ItemTyoe
    {
        Item1,
        Item2,
        Item3
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public interface IInteractable
    {
        public bool Interact(GameObject user);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public interface IInteractable
    {
        public string Interact(GameObject user);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class UsableItem : Item, IUsableItem
    {
        public virtual void SetData(params object[] args)
        {

        }

        public virtual bool Use(GameObject user)
        {
            return false;
        }
    }
}


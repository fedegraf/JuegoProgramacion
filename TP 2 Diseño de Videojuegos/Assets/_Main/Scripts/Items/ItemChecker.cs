using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemChecker : MonoBehaviour
    {
        [SerializeField] private ItemTypeSO itemToCheck;
        [SerializeField] private int ammountNeeded;

        //checks if the player has the item and the ammount needed

        private void CheckItems(List<IItem> itemList)
        {
            int ammontInInventory = 0;

            for (int i = 0; i < itemList.Count; i++)
            {
                var currentItem = itemList[i];
                if (currentItem.Data == itemToCheck)
                {
                    ammontInInventory++;
                    if (ammontInInventory == ammountNeeded)
                    {
                        //Trigger Level Passed
                        Debug.Log($"Level Passed, {ammountNeeded} {itemToCheck.ItemName} collected");
                    }
                }                    
            }

            if (ammontInInventory < ammountNeeded)
                Debug.Log($"Search for more {itemToCheck.ItemName}, you need {ammountNeeded - ammontInInventory} more");
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IItemPickUp>(out var itemPickUp))
            {
                
                return;
            }

            CheckItems(itemPickUp.ItemsInInventory);
        }
    }
}
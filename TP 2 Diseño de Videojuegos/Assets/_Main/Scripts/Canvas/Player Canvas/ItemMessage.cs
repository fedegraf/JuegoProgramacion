using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class ItemMessage : MonoBehaviour
    {
        [SerializeField] private string itemName;
        [SerializeField] private string interactionMessage;
        [SerializeField] private TextMesh itemNameText;
        [SerializeField] private TextMesh interactionMessageText;

        private void Start()
        {
            ToggleInfoText(false);

            itemNameText.text = itemName;
            interactionMessageText.text = interactionMessage;
        }

        void ToggleInfoText(bool isActive)
        {
            interactionMessageText.gameObject.SetActive(isActive);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                ToggleInfoText(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                ToggleInfoText(false);
        }


    }

}
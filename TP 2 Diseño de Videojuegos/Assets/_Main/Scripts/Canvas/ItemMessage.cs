using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class ItemMessage : MonoBehaviour
    {
        [SerializeField] private string _nearMessage;
        [SerializeField] private TextMesh text;
        private string awayMessage;

        private void Awake()
        {
            awayMessage = GetComponent<IItem>().Data.ItemName;
        }

        private void Start()
        {
            ChangeMessage(awayMessage);
        }

        void ChangeMessage(string message)
        {
            text.text = message;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                ChangeMessage(_nearMessage);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                ChangeMessage(awayMessage);
        }


    }

}
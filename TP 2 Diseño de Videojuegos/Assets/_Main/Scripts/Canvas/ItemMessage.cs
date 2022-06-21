using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class ItemMessage : MonoBehaviour
    {
        [SerializeField] private string nearMessage;
        [SerializeField] private string awayMessage;
        [SerializeField] private TextMesh text;

        private void Awake()
        {
            if(awayMessage == "")
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
                ChangeMessage(nearMessage);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                ChangeMessage(awayMessage);
        }


    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class OrisusInfo : MonoBehaviour, IObserver
    {
        [SerializeField] private Text ammountText;

        private void Awake()
        {
            GetComponent<ItemLooter>().Suscribe(this);
            SetText(0);
        }

        public void OnNotify(string message, params object[] args)
        {
            if (message != "ITEMCOLLECTED") return;
            {
                SetText((int)args[0]);
            }

        }

        private void SetText(int ammount)
        {
            ammountText.text = $"{ammount}/3";
        }

    }
}

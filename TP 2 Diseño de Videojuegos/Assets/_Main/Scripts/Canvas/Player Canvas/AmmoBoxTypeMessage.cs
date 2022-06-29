using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class AmmoBoxTypeMessage : MonoBehaviour
    {
        [SerializeField] private TextMesh itemText;
        private AmmoBox _ammoBox;

        private void Awake()
        {
            _ammoBox = GetComponent<AmmoBox>();
        }

        private void Start()
        {
            itemText.text = _ammoBox.BulletType.BulletName;
        }
    }

}
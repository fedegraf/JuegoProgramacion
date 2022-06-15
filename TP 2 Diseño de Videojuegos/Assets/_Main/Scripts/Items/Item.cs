using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField] private ItemTypeSO data;
        public GameObject ItemObject => gameObject;
        public ItemTypeSO Data => data;
        public bool PickUpInRange { get; private set; }

        private SphereCollider _sphereColldr;
        private MeshRenderer _meshRndr;
        private MeshFilter _meshFiltr;

        private void Awake()
        {
            _sphereColldr = GetComponent<SphereCollider>();
            _meshRndr = GetComponent<MeshRenderer>();
            _meshFiltr = GetComponent<MeshFilter>();
        }

        private void Start()
        {
            if (Data != null)
            {
                transform.localScale = Data.Scale;
                _meshRndr.material.color = Data.Color;
                _meshFiltr.mesh = Data.Mesh;
                _sphereColldr.radius = Data.InteractionRadius;
            }
            else
                Debug.LogWarning("Item Data is Not Set!");
        }

        public void SetData(ItemTypeSO newData)
        {
            data = newData;
        }

        public IItem PickUp()
        {
            ItemObject.SetActive(false);
            Destroy(gameObject);
            return this;
        }
    }
}
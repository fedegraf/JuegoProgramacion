using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{ 
    public class PickUpalbeItem : MonoBehaviour, IPickUpable
    {
        [SerializeField] private float interactableRadius = 1f;
        [SerializeField] private string itemName;
        [SerializeField] private ItemTyoe itemType;

        private SphereCollider _sphrCollider;

        public string ItemName => itemName;

        public ItemTyoe ItemTyoe => itemType;

        private void Awake()
        {
            _sphrCollider = GetComponent<SphereCollider>();
        }

        private void Start()
        {
            _sphrCollider.radius = interactableRadius;
        }


        public IPickUpable PickedUp()
        {
            Destroy(gameObject);
            return this;
        }

    }
}

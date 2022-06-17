using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class BaseItem : MonoBehaviour, IItem
    {
        [Header("Base Settings")]
        [SerializeField] private ItemTypeSO data;

        public ItemTypeSO Data => data;
        public GameObject ItemObject => gameObject;

        private SphereCollider _sphrCollider;
        private MeshFilter _meshFiltr;
        private MeshRenderer _meshRndr;

        private void Awake()
        {
            if (Data == null) return;

            _sphrCollider = GetComponent<SphereCollider>();
            _meshFiltr = GetComponent<MeshFilter>();
            _meshRndr = GetComponent<MeshRenderer>();


            _sphrCollider.radius = Data.InteractionRadius;
            _meshFiltr.mesh = Data.Mesh;
            _meshRndr.material = Data.Material;
            transform.localScale = Data.Scale;
        }

        public void SetData(ItemTypeSO newData)
        {
            data = newData;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, Data.InteractionRadius);
        }
    }
}

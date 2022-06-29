using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class BaseItem : MonoBehaviour, IItem
    {
        [Header("Base Settings")]
        [SerializeField] private ItemTypeSO data;
        [SerializeField] private float rotatinSpeed;
        [SerializeField] private bool canRotate;
        //[SerializeField] private float rotationSpeed;
        public ItemTypeSO Data => data;
        public GameObject ItemObject => gameObject;

        private SphereCollider _sphrCollider;
        private GameObject _mesh;
        private MeshFilter _meshFiltr;
        private MeshRenderer _meshRndr;

        private void Awake()
        {
            if (Data == null) return;

            _sphrCollider = GetComponent<SphereCollider>();;
            _mesh = transform.GetChild(0).gameObject;
            _meshFiltr = _mesh.GetComponent<MeshFilter>();
            _meshRndr = _mesh.GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            if (canRotate)
            {
                transform.Rotate(Vector3.up, rotatinSpeed * Time.deltaTime);
            }
        }
        public void SetData(ItemTypeSO newData)
        {
            data = newData;
            _sphrCollider.radius = Data.InteractionRadius;
            _meshFiltr.mesh = Data.Mesh;
            _meshRndr.material = Data.Material;
            transform.localScale = Data.Scale;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, Data.InteractionRadius);
        }
    }
}

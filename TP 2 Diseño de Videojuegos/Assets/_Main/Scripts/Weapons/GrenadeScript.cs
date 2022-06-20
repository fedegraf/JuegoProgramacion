using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class GrenadeScript : BulletBase
    {
        [SerializeField] private float explosionRadius;
        [SerializeField] private float explosionForce;
        private Rigidbody _rb;
        

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_currentLifeTime <= 0)
            {
                Debug.Log("Explode");
            }
                
        }

        private void Start()
        {
            Throw();
        }


        public void Throw()
        {
            var direction = (transform.forward + (Vector3.up * 0.5f)) * Data.Speed;
            _rb.AddForce(direction, ForceMode.Impulse);
        }

        private void Explode()
        {
            var newSphere = gameObject.AddComponent<SphereCollider>();
            newSphere.isTrigger = true;
            newSphere.radius = explosionRadius;
            DestroyBullet();
        }

        private void OnTriggerEnter(Collider other)
        {
            var rigidBody = other.GetComponent<Rigidbody>();
            if (!rigidBody) return;

            rigidBody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}

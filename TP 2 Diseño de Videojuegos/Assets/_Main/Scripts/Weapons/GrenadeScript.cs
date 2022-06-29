using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class GrenadeScript : BulletBase
    {
        [SerializeField] private float explosionRadius;
        [SerializeField] private float explosionForce;
        [SerializeField] private GameObject BlastFx;
        private Rigidbody _rb;

        private List<IDamagable> _damagablesInRange = new List<IDamagable>();

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public override void Update()
        {
            base.Update();
            if (_currentLifeTime <= 0)
                Explode();
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
            Debug.Log("EXPLOSION");
            Instantiate(BlastFx, transform.position, transform.rotation);
            GetComponentInParent<AudioSource>().Play();
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider nearbyObject in colliders)
            {
                if (nearbyObject.TryGetComponent<IDamagable>(out var damagable))
                {
                    damagable.TakeDamage(ScriptableObjects.BlastDamage);
                    nearbyObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce,
                        transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
                }
            }
            DestroyBullet();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}

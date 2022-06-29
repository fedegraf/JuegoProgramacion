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
        private bool canThrow = true;
        private GameObject _grenade;

        private bool _hasExplode;

        private List<IDamagable> _damagablesInRange = new List<IDamagable>();

        private SoundManager _sounds;

        private float currentTime;
        private float afterExplosionMaxTime = 2f;

        private void Awake()
        {
            _sounds = GetComponent<SoundManager>();
            _grenade = transform.GetChild(0).gameObject;
            currentTime = afterExplosionMaxTime;
        }

        public override void Update()
        {
            base.Update();
            if (_currentLifeTime <= 0 && !_hasExplode)
                Explode();
            if (_hasExplode)
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                    DestroyBullet();
            }
        }

        private void Start()
        {
            if(canThrow)
                Throw();
        }


        public void Throw()
        {
            var direction = (transform.forward + (Vector3.up * 0.5f)) * Data.Speed;
            _grenade.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
        }

        public void ToggleCanThrow()
        {
            canThrow = false;
        }

        public void Explode()
        {
            transform.position = _grenade.transform.position;
            _hasExplode = true;
            _sounds.PlaySound("Explotion");
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
            _grenade.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}

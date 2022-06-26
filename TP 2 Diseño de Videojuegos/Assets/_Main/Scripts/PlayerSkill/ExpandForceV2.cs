using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills
{
    public class ExpandForceV2 : MonoBehaviour
    {
        [SerializeField] private float force;
        [SerializeField] private float expansionRadius;
        [SerializeField] private float expansionSpeed;
        [SerializeField] private float maxTimeExpanded;
        [SerializeField] private int damage;

        private float _currentTimeExpanded;
        public bool IsSphereExpanded { get; private set; }

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (IsSphereExpanded)
            {
                _currentTimeExpanded += Time.deltaTime;
                if (_currentTimeExpanded >= maxTimeExpanded)
                {
                    ResetExpandedTime();
                    StartCoroutine(ShrinkSphere());
                }
            }
        }

        public void DoStartForce()
        {
            if (IsSphereExpanded) return;

            StartCoroutine(ExpandSphere());
        }

        private void ResetExpandedTime()
        {
            _currentTimeExpanded = 0;
        }

        private void UseForce(Rigidbody body)
        {
            body.AddForce(-body.transform.forward * force, ForceMode.VelocityChange);          
        }

        private void DamageBody(IDamagable damagable)
        {
            damagable.TakeDamage(damage);
        }

        private IEnumerator ExpandSphere()
        {
            var sphreScale = transform.localScale.x;
            gameObject.SetActive(true);

            while (expansionRadius > sphreScale)
            {

                sphreScale += (Time.deltaTime * expansionSpeed);

                var newScale = new Vector3(sphreScale, sphreScale, sphreScale);

                gameObject.transform.localScale = newScale;

                if (sphreScale >= expansionRadius) IsSphereExpanded = true;

                yield return null;
            }

            yield return null;
        }

        private IEnumerator ShrinkSphere()
        {
            var sphreScale = gameObject.transform.localScale.x;

            while (sphreScale > 0)
            {
                sphreScale -= (Time.deltaTime * expansionSpeed / 1.75f);

                var newScale = new Vector3(sphreScale, sphreScale, sphreScale);

                gameObject.transform.localScale = newScale;

                if (sphreScale <= 0)
                {
                    gameObject.SetActive(false);
                    IsSphereExpanded = false;
                }

                yield return null;
            }

            yield return null;
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<IEnemy>();

            if (enemy == null || enemy.IsDead) return;

            UseForce(other.GetComponent<Rigidbody>());
            DamageBody(other.GetComponent<Damagable>());
        }
    }
}

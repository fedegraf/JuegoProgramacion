using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponBullet : BulletBase, IProduct<AmmoTypeSO>
    {
        [SerializeField] private GameObject particleEffect;

        public override void Update()
        {
            base.Update();
            if (_currentLifeTime > 0)
                Movement();
            else
                DestroyBullet();
        }

        private void Movement()
        {
            float finalSpeed = Data.Speed * Time.deltaTime;
            transform.position += transform.forward * finalSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wall") || other.CompareTag("Prop"))
            {
                Instantiate(particleEffect, transform.position, transform.rotation);
                DestroyBullet();
            }
                
        }
    }
}

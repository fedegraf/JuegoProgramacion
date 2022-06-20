using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class BulletBase : MonoBehaviour, IProduct<AmmoTypeSO>
    {
        public AmmoTypeSO Data { get; private set; }
        protected float _currentLifeTime;

        private void Awake()
        {
            GetComponent<Damager>().SetDamage(Data.Damage);
        }

        private void Update()
        {
            if (_currentLifeTime > 0)
                _currentLifeTime -= Time.deltaTime;

            Debug.Log(_currentLifeTime);
        }

        public void SetData(AmmoTypeSO newData)
        {
            Data = newData;
            _currentLifeTime = Data.LifeTime;
        }
        protected void DestroyBullet()
        {
            Destroy(gameObject);
        }
    }
}

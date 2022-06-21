using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class BulletBase : MonoBehaviour, IProduct<AmmoTypeSO>
    {
        public AmmoTypeSO Data { get; private set; }
        protected float _currentLifeTime;


        public virtual void Update()
        {
            if (_currentLifeTime > 0)
                _currentLifeTime -= Time.deltaTime;
        }

        public void SetData(AmmoTypeSO newData)
        {
            Data = newData;
            _currentLifeTime = Data.LifeTime;
            GetComponent<Damager>().SetDamage(Data.Damage);
        }
        protected void DestroyBullet()
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class TakeAmmoCommand : ICommand
    {
        private AmmoController _ammo;
        private BulletTypeSO _bulletType;
        private int _ammoToAdd;

        public TakeAmmoCommand(AmmoController ammo, BulletTypeSO bulletType, int ammoToAdd)
        {
            _ammo = ammo;
            _bulletType = bulletType;
            _ammoToAdd = ammoToAdd;
        }

        public void Do()
        {
            int ammoCollected = _ammo.AmmoCollected[_bulletType];

            if (ammoCollected >= _bulletType.MaxAmmo) return;

            _ammo.AddAmmo(_bulletType, _ammoToAdd);
        }
    }
}

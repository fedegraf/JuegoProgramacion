using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class AmmoBox : Item, IUsableItem
    {
        [SerializeField] private int ammoAmmount;
        [SerializeField] private Shooter.BulletTypeSO bulletType;
        private Shooter.TakeAmmoCommand _takeAmmoCommand;

        public bool Use(GameObject user)
        {
            if (!user.TryGetComponent<IDamagable>(out var ammoController)) return false;

            _takeAmmoCommand = new Shooter.TakeAmmoCommand(user.GetComponent<Shooter.AmmoController>(), bulletType, ammoAmmount);

            if (_takeAmmoCommand == null) return false;

            _takeAmmoCommand.Do();
            gameObject.SetActive(false);

            return true;
        }
    }
}

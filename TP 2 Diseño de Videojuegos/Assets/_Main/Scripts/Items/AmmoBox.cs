using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class AmmoBox : BaseItem, IInteractable
    {
        [SerializeField] private int ammoAmmount;
        [SerializeField] private Shooter.BulletTypeSO bulletType;
        private Shooter.TakeAmmoCommand _command;

        public bool Interact(GameObject user)
        {
            if (!user.TryGetComponent<Shooter.AmmoController>(out var ammo)) return false;

            if (ammo.IsAmmoFull(bulletType)) return false;

            _command = new Shooter.TakeAmmoCommand(ammo, bulletType, ammoAmmount);
            _command.Do();
            gameObject.SetActive(false);
            return true;
        }
    }

}
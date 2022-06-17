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
            if (!user.TryGetComponent<Shooter.WeaponController>(out var weapon)) return false;

            if (weapon.Ammo.CanReload(weapon.CurrentWeapon)) return false;

            _command = new Shooter.TakeAmmoCommand(weapon.Ammo, bulletType, ammoAmmount);
            _command.Do();
            gameObject.SetActive(false);
            return true;
        }
    }

}
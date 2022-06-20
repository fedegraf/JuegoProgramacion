using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class AmmoBox : BaseItem, IInteractable
    {
        [SerializeField] private int ammoAmmount;
        [SerializeField] private Weapons.AmmoTypeSO bulletType;
        private Weapons.TakeAmmoCommand _command;

        public Weapons.AmmoTypeSO BulletType => bulletType;

        public bool Interact(GameObject user)
        {
            if (!user.TryGetComponent<Weapons.WeaponController>(out var weapon)) return false;

            if (weapon.Ammo.CanReload(weapon.CurrentWeapon)) return false;

            _command = new Weapons.TakeAmmoCommand(weapon.Ammo, bulletType, ammoAmmount);
            _command.Do();
            gameObject.SetActive(false);
            return true;
        }
    }

}
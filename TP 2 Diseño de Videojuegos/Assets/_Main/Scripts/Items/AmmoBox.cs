using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class AmmoBox : BaseItem, IInteractable
    {
        [SerializeField] private int ammoAmmount;
        [SerializeField] private Weapons.AmmoTypeSO ammoType;
        private Weapons.TakeAmmoCommand _command;

        public Weapons.AmmoTypeSO BulletType => ammoType;

        public string Interact(GameObject user)
        {
            if (!user.TryGetComponent<Weapons.WeaponController>(out var weapon)) return "You can't get ammo";

            if (weapon.Ammo.CanGetAmmo(weapon.CurrentWeapon)) return "You've reached max ammount for this ammo";

            _command = new Weapons.TakeAmmoCommand(weapon.Ammo, ammoType, ammoAmmount);
            _command.Do();
            gameObject.SetActive(false);
            return $"You got {ammoAmmount} {ammoType.BulletName} ammo";
        }

        public void SetValues(Weapons.AmmoTypeSO ammoToSet, int ammoAmmount)
        {
            ammoType = ammoToSet;
            this.ammoAmmount = ammoAmmount;
        }
    }

}
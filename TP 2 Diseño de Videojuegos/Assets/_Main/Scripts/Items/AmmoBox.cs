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


        public object[] Test(GameObject user)
        {
            object[] values = new object[5];

            if (!user.TryGetComponent<Weapons.WeaponController>(out var weapon))
            {
                values[0] = "You can't get ammo";
                values[1] = false;
                return values;
            }

            if (weapon.Ammo.IsAmmoFull(ammoType))
            {
                values[0] = "You've reached max ammount for this ammo";
                values[1] = false;
                return values;
            }

            _command = new Weapons.TakeAmmoCommand(weapon.Ammo, ammoType, ammoAmmount);
            _command.Do();
            gameObject.SetActive(false);

            values[0] = $"You got {ammoAmmount} {ammoType.BulletName} ammo";
            values[1] = true;
            return values;
        }

        public string Interact(GameObject user)
        {
            if (!user.TryGetComponent<Weapons.WeaponController>(out var weapon)) return "You can't get ammo";

            if (weapon.Ammo.IsAmmoFull(ammoType)) return "You've reached max ammount for this ammo";

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class WeaponItem : BaseItem, IInteractable
    {
        [SerializeField] private Weapons.WeaponTypeSO weaponType;
        private Weapons.AddWeaponCommand _command;

        public Weapons.WeaponTypeSO WeaponType => weaponType;



        public string Interact(GameObject user)
        {
            if (!user.TryGetComponent<Weapons.WeaponController>(out var wpnController)) return "Can't equip this weapon";

            _command = new Weapons.AddWeaponCommand(wpnController, weaponType);
            _command.Do();
            gameObject.SetActive(false);
            return $"You got a {weaponType.WeaponName}!";
        }

        public object[] Test(GameObject user)
        {
            object[] values = new object[5];

            if (!user.TryGetComponent<Weapons.WeaponController>(out var wpnController))
            {
                values[0] = "Can't equip this weapon";
                values[1] = false;
                return values;
            }

            _command = new Weapons.AddWeaponCommand(wpnController, weaponType);
            _command.Do();
            gameObject.SetActive(false);
            values[0] = $"You got a {weaponType.WeaponName}!";
            values[1] = true;
            return values;
        }
    }
}

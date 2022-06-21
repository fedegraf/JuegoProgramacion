using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class AddWeaponCommand : ICommand
    {
        private WeaponController _wpnController;
        private WeaponTypeSO _weaponToAdd;

        public AddWeaponCommand(WeaponController wpnController, WeaponTypeSO weaponToAdd)
        {
            _wpnController = wpnController;
            _weaponToAdd = weaponToAdd;
        }

        public void Do()
        {
            int weaponIndex = _wpnController.WeaponsList.Count + 1;
            var newWeapon = new Weapon(_weaponToAdd, weaponIndex);
            _wpnController.AddWeapon(newWeapon);
        }
    }
}

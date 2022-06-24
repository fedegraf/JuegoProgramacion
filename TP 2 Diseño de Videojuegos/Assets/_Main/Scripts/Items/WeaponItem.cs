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
    }
}

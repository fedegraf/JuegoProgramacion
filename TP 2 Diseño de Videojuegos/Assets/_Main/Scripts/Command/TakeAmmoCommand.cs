using UnityEngine;

namespace Weapons
{
    public class TakeAmmoCommand : ICommand
    {
        private AmmoController _ammo;
        private AmmoTypeSO _ammoType;
        private int _ammoToAdd;

        public TakeAmmoCommand(AmmoController ammo, AmmoTypeSO ammoType, int ammoToAdd)
        {
            _ammo = ammo;
            _ammoType = ammoType;
            _ammoToAdd = ammoToAdd;
        }

        public void Do()
        {
            int currentAmmo = _ammo.GetAmmo(_ammoType);
            int newTotalAmmo = _ammoToAdd + currentAmmo;
            if (newTotalAmmo > _ammoType.MaxAmmo)
            { 
                int temp = newTotalAmmo - _ammoType.MaxAmmo;
                _ammoToAdd -= temp;
            }
            _ammo.AddAmmo(_ammoType, _ammoToAdd);
        }
    }
}

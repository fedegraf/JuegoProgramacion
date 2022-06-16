using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private WeaponTypeSO[] weapons;

        private int _currentWeaponIndex;
        private Dictionary<WeaponTypeSO, int> _ammoInWeaponsMag = new Dictionary<WeaponTypeSO, int>();
        //WeaponTypeSO = Weapon
        //Int = AmmoInMag

        public WeaponTypeSO CurrentWeapon { get; private set; }

        private void Start()
        {
            SetWeaponDictionary();
        }

        private void SetWeaponDictionary()
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                _ammoInWeaponsMag.Add(weapons[i], weapons[i].MagazineSize);
            }

            CurrentWeapon = weapons[0];
        }

        public void SetAmmo(int newAmmo)
        {
            _ammoInWeaponsMag[CurrentWeapon] = newAmmo;
        }

        public void ShootAmmo()
        {
            _ammoInWeaponsMag[CurrentWeapon]--;
        }

        public void CycleWeapons()
        {
            _currentWeaponIndex++;

            if (_currentWeaponIndex >= weapons.Length)
                _currentWeaponIndex = 0;

            CurrentWeapon = weapons[_currentWeaponIndex];
        }

        public int GetCurrentAmmoInMag()
        {
            if (_ammoInWeaponsMag.ContainsKey(CurrentWeapon))
                return _ammoInWeaponsMag[CurrentWeapon];
            else
                return 0;
        }
    }
}

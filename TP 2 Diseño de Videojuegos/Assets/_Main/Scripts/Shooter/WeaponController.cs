using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private WeaponTypeSO[] weapons;

        private Dictionary<WeaponTypeSO, int> _weapons = new Dictionary<WeaponTypeSO, int>();
        //WeaponTypeSO = Weapon
        //Int = AmmoInMag

        public WeaponTypeSO CurrentWeapon { get; private set; }
        public int AmmoInMag { get; private set; }

        private void Start()
        {
            SetWeaponDictionary();
        }

        private void SetWeaponDictionary()
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                _weapons.Add(weapons[i], weapons[i].MagazineSize);
            }

            CurrentWeapon = weapons[0];
        }

        public void SetAmmo(int newAmmo)
        {
            _weapons[CurrentWeapon] = newAmmo;
        }

        public void ShootAmmo()
        {
            _weapons[CurrentWeapon]--;
        }

        public int GetCurrentAmmoInMag()
        { 
            return _weapons[CurrentWeapon];
        }

    }
}

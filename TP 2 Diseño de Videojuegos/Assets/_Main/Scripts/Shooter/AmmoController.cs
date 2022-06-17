using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class AmmoController : MonoBehaviour
    {
        private Dictionary<BulletTypeSO, int> _ammoCollected = new Dictionary<BulletTypeSO, int>();

        public Dictionary<BulletTypeSO, int> AmmoCollected => _ammoCollected;

        public void AddAmmo(BulletTypeSO bulletType, int ammoToAdd)
        {
            if (AmmoCollected.ContainsKey(bulletType))
            {
                int currntAmmo = AmmoCollected[bulletType];
                currntAmmo += ammoToAdd;

                if (currntAmmo > bulletType.MaxAmmo)
                {
                    currntAmmo = bulletType.MaxAmmo;
                }

                AmmoCollected[bulletType] = currntAmmo;
            }
            else
            { 
               AmmoCollected.Add(bulletType, ammoToAdd);
            }
        }

        public int GetAmmo(WeaponController wpnCntrl)
        {
            if (wpnCntrl.CurrentWeapon == null) return 0;

            var ammoType = wpnCntrl.CurrentWeapon.Data.BulletType;
            if (AmmoCollected.ContainsKey(ammoType))
                return AmmoCollected[ammoType];
            else
                return 0;
        }

        public bool ReloadAmmo(WeaponController wpnController)
        {    
            if (GetAmmo(wpnController) == 0)
            {
                Debug.Log("There is not ammo for this weapon");
                return false;
            }

            var currentWeapon = wpnController.CurrentWeapon;

            int ammoToReload = currentWeapon.Data.MagazineSize - wpnController.GetCurrentAmmoInMag();
            int ammoInBag = AmmoCollected[currentWeapon.Data.BulletType];
            if (ammoInBag < ammoToReload)
            {
                ammoToReload = ammoInBag;
                AmmoCollected[currentWeapon.Data.BulletType] = 0;
            }

            AmmoCollected[currentWeapon.Data.BulletType] -= ammoToReload;

            wpnController.SetAmmo(ammoToReload);

            return true;
        }

    }
}

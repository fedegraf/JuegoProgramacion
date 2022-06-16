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
            var ammoType = wpnCntrl.CurrentWeapon.BulletType;
            return AmmoCollected[ammoType];
        }

        public void ReloadAmmo(WeaponController wpnController)
        {    
            if (GetAmmo(wpnController) == 0)
            {
                Debug.Log("There is not ammo for this weapon");
                return;
            }

            var currentWeapon = wpnController.CurrentWeapon;

            int ammoToReload = currentWeapon.MagazineSize - wpnController.AmmoInMag;
            int ammoInBag = AmmoCollected[currentWeapon.BulletType];
            if (ammoInBag < ammoToReload)
            {
                ammoToReload = ammoInBag;
                AmmoCollected[currentWeapon.BulletType] = 0;
            }

            AmmoCollected[currentWeapon.BulletType] -= ammoToReload;

            wpnController.SetAmmo(ammoToReload);
        }

    }
}

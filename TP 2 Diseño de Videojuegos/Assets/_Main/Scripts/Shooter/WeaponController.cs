using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private WeaponTypeSO[] weaponsType;
        [SerializeField] private GameObject weaponGO;

        private List<Weapon> _weaponsList = new List<Weapon>();
        public Weapon CurrentWeapon { get; private set; }

        private void Awake()
        {
            SetWeaponsList();

        }

        private void SetWeaponsList()
        {
            for (int i = 0; i < weaponsType.Length; i++)
            {
                var newWeapon = new Weapon(weaponsType[i],i);
                _weaponsList.Add(newWeapon);
            }

            SetWeapon(0);
        }

        private void SetWeapon(int index)
        {
            CurrentWeapon = _weaponsList[index];
            if (CurrentWeapon == null) return;

            var weaponSMR = weaponGO.GetComponent<SkinnedMeshRenderer>();
            weaponSMR.sharedMesh = CurrentWeapon.Data.Mesh;
        }

        public void SetAmmo(int newAmmo)
        {
            CurrentWeapon.SetAmmoInMag(newAmmo);
        }

        public void ShootAmmo()
        {
            CurrentWeapon.Shoot();
        }

        public void CycleWeapons()
        {
            int currentIndex = CurrentWeapon.Index;
            currentIndex++;

            if (currentIndex >= _weaponsList.Count)
                currentIndex = 0;

            SetWeapon(currentIndex);
        }

        public int GetCurrentAmmoInMag()
        {
            if (CurrentWeapon != null)
                return CurrentWeapon.AmmoInMag;
            else
                return 0;
        }
    }

    public class Weapon
    {
        public WeaponTypeSO Data { get; private set; }
        public int Index { get; private set; }
        public  int AmmoInMag { get; private set; }

        public Weapon(WeaponTypeSO type,int index)
        {
            Data = type;
            Index = index;
            AmmoInMag = Data.MagazineSize;
        }

        public void SetAmmoInMag(int ammo)
        {
            AmmoInMag = ammo;
        }

        public void Shoot()
        {
            AmmoInMag--;
        }
    }
}

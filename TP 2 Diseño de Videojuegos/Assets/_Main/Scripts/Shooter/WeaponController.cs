using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class WeaponController : BulletSpawner, IObservable
    {
        [Header("Weapon Values")]
        [SerializeField] private WeaponTypeSO[] weaponsType;
        [SerializeField] private GameObject weaponGO;

        private float _currentShootCD;
        private float _currentReloadCD;
        private List<IObserver> _subscribers = new List<IObserver>();
        private List<Weapon> _weaponsList = new List<Weapon>();
        private List<Bullet> _bulletsInWorld = new List<Bullet>();
        

        public Weapon CurrentWeapon { get; private set; }
        public AmmoController Ammo { get; private set; }
        public bool CanShoot { get; private set;}
        public bool IsShooting => _currentShootCD > 0;
        public bool IsReloading => _currentReloadCD > 0;
        public List<IObserver> Subscribers => _subscribers;


        private void Awake()
        {
            Ammo = new AmmoController();
            SetWeaponsList();
        }

        private void Start()
        {
            EnableShooting(true);
        }

        private void Update()
        {
            if (_currentShootCD > 0)
                _currentShootCD -= Time.deltaTime;
            if (_currentReloadCD > 0)
            {
                _currentReloadCD -= Time.deltaTime;
                if (_currentReloadCD <= 0)
                    UpdateAmmoHud();
            }
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

            UpdateAmmoHud();
            UpdateWeaponHud();
        }

        private void UpdateAmmoHud() => NotifyAll("AMMOUPDATE", CurrentWeapon.AmmoInMag, Ammo.GetAmmo(CurrentWeapon));
        private void UpdateWeaponHud() => NotifyAll("WEAPONUPDATE", CurrentWeapon.Data.WeaponName);

        private void ResetShootCoolDown() => _currentShootCD = CurrentWeapon.Data.Cadence;
        private void ResetReloadCoolDown() => _currentReloadCD = CurrentWeapon.Data.ReloadTime;

        public void SetAmmo(int newAmmo)
        {
            CurrentWeapon.SetAmmoInMag(newAmmo);
        }

        public void EnableShooting(bool enable)
        {
            CanShoot = enable;
        }

        public void DoShoot()
        {
            if (CurrentWeapon.AmmoInMag == 0)
            {
                Ammo.Reload(CurrentWeapon);
                return;
            }

            if (!CanShoot || IsShooting || IsReloading || _currentShootCD > 0) return;

            _bulletsInWorld.Add(CreateBullet(CurrentWeapon.Shoot()));
            UpdateAmmoHud();
            ResetShootCoolDown();
        }

        public void DoReload()
        {
            if (CurrentWeapon.AmmoInMag == CurrentWeapon.Data.MagazineSize || IsReloading || IsShooting || !Ammo.CanReload(CurrentWeapon)) return;

            Ammo.Reload(CurrentWeapon);
            NotifyAll("RELOAD");
            ResetReloadCoolDown();
        }

        public void DoCycleWeapons()
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

        public void Suscribe(IObserver observer)
        {
            if (_subscribers.Contains(observer)) return;
            _subscribers.Add(observer);
        }

        public void Unsuscribe(IObserver observer)
        {
            if (!_subscribers.Contains(observer)) return;
            _subscribers.Remove(observer);
        }

        public void NotifyAll(string message, params object[] args)
        {
            if (_subscribers.Count < 1) return;

            foreach (var suscriber in _subscribers)
            {
                suscriber.OnNotify(message, args);
            }
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

        public BulletTypeSO Shoot()
        {
            AmmoInMag--;
            return Data.BulletType;
        }
    }

    public class AmmoController
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
        public int GetAmmo(Weapon weapon)
        {
            if (weapon == null) return 0;

            var ammoType = weapon.Data.BulletType;
            if (AmmoCollected.ContainsKey(ammoType))
                return AmmoCollected[ammoType];
            else
                return 0;
        }

        public bool CanReload(Weapon weapon)
        {
            int currentAmmo = GetAmmo(weapon);
            int maxAmmo = weapon.Data.BulletType.MaxAmmo;
            return currentAmmo > 0 && currentAmmo < maxAmmo;
        }

        public void Reload(Weapon weapon)
        {
            int ammoToReload = weapon.Data.MagazineSize - weapon.AmmoInMag;
            int ammoInBag = AmmoCollected[weapon.Data.BulletType];
            if (ammoInBag < ammoToReload)
            {
                ammoToReload = ammoInBag;
                AmmoCollected[weapon.Data.BulletType] = 0;
            }

            AmmoCollected[weapon.Data.BulletType] -= ammoToReload;

            weapon.SetAmmoInMag(ammoToReload);
        }

    }
}
 
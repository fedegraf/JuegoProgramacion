using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
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
        private List<BulletBase> _bulletsInWorld = new List<BulletBase>();       

        public Weapon CurrentWeapon { get; private set; }
        public AmmoController Ammo { get; private set; }
        public List<Weapon> WeaponsList => _weaponsList;
        public bool CanShoot { get; private set;}
        public bool IsShooting => _currentShootCD > 0;
        public bool IsReloading => _currentReloadCD > 0;
        public List<IObserver> Subscribers => _subscribers;

        //Test 
        [Header("Test Values")]
        [SerializeField] AmmoTypeSO[] ammoTypeToAdd;
        [SerializeField] int[] ammoAmmountToAdd;
        private TakeAmmoCommand _takeAmmoCommand;


        private void Awake()
        {
            Ammo = new AmmoController();
            Ammo.OnAmmoAdded += OnAmmoAddedHandler;
        }

        private void Start()
        {
            EnableShooting(true);
            SetWeaponsList();
            AddAmmo();
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

        private void AddAmmo()
        {
            for (int i = 0; i < ammoTypeToAdd.Length; i++)
            {
                _takeAmmoCommand = new TakeAmmoCommand(Ammo, ammoTypeToAdd[i], ammoAmmountToAdd[i]);
                _takeAmmoCommand.Do();
            }
        }

        private void SetWeaponsList()
        {
            if (weaponsType.Length < 1) return;

            for (int i = 0; i < weaponsType.Length; i++)
            {
                var newWeapon = new Weapon(weaponsType[i],i);
                AddWeapon(newWeapon);
            }
        }

        private void SetWeapon(int index)
        {
            CurrentWeapon = _weaponsList[index];
            if (CurrentWeapon == null) return;

            var weaponSMR = weaponGO.GetComponent<SkinnedMeshRenderer>();
            if (!weaponSMR) return; 
            weaponSMR.sharedMesh = CurrentWeapon.Data.Mesh;

            UpdateAmmoHud();
            UpdateWeaponHud();
        }

        private void UpdateAmmoHud()
        {
            if (CurrentWeapon == null) return;

            NotifyAll("AMMOUPDATE", CurrentWeapon.AmmoInMag, Ammo.GetAmmo(CurrentWeapon.Data.AmmoType));
        }

        private void UpdateWeaponHud()
        {
            if (CurrentWeapon == null) return;

            NotifyAll("WEAPONUPDATE", CurrentWeapon.Data.WeaponName);
        }

        private void ResetShootCoolDown() => _currentShootCD = CurrentWeapon.Data.Cadence;
        private void ResetReloadCoolDown() => _currentReloadCD = CurrentWeapon.Data.ReloadTime;

        private void OnAmmoAddedHandler()
        {
            UpdateAmmoHud();
        }

        public void SetAmmo(int newAmmo)
        {
            CurrentWeapon.SetAmmoInMag(newAmmo);
        }

        public void EnableShooting(bool enable)
        {
            CanShoot = enable;
        }

        public void AddWeapon(Weapon newWeapon)
        {
            _weaponsList.Add(newWeapon);
            if (_weaponsList.Count == 1)
                SetWeapon(0);
        }

        public void DoShoot()
        {
            if (CurrentWeapon == null) return;

            if (CurrentWeapon.AmmoInMag == 0)
            {
                DoReload();
                return;
            }

            if (!CanShoot || IsShooting || IsReloading || _currentShootCD > 0) return;

            _bulletsInWorld.Add(CreateBullet(CurrentWeapon.Shoot()));
            UpdateAmmoHud();
            ResetShootCoolDown();
        }

        public void DoReload()
        {
            if (CurrentWeapon == null) return;

            if (CurrentWeapon.AmmoInMag == CurrentWeapon.Data.MagazineSize || IsReloading || IsShooting || !Ammo.CanReload(CurrentWeapon)) return;

            Ammo.Reload(CurrentWeapon);
            NotifyAll("RELOAD");
            ResetReloadCoolDown();

        }

        public void DoCycleWeapons()
        {
            if (CurrentWeapon == null || IsReloading) return;

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

        public AmmoTypeSO Shoot()
        {
            AmmoInMag--;
            return Data.AmmoType;
        }
    }

    public class AmmoController
    {
        private Dictionary<AmmoTypeSO, int> _ammoCollected = new Dictionary<AmmoTypeSO, int>();

        public Dictionary<AmmoTypeSO, int> AmmoCollected => _ammoCollected;

        public event Action OnAmmoAdded;

        public void AddAmmo(AmmoTypeSO bulletType, int ammoToAdd)
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

            OnAmmoAdded.Invoke();

        }
        public int GetAmmo(AmmoTypeSO ammoType)
        {
            if (ammoType == null) return 0;

            if (AmmoCollected.ContainsKey(ammoType))
                return AmmoCollected[ammoType];
            else
                return 0;
        }

        public bool IsAmmoFull(Weapon weapon)
        { 
            int currentAmmo = GetAmmo(weapon.Data.AmmoType);
            return currentAmmo >= weapon.Data.AmmoType.MaxAmmo;
        }

        public bool CanReload(Weapon weapon)
        {
            int currentAmmo = GetAmmo(weapon.Data.AmmoType);
            if (currentAmmo == 0) return false;
            return currentAmmo > 0;
        }

        public void Reload(Weapon weapon)
        {
            int ammoNeeded = weapon.Data.MagazineSize - weapon.AmmoInMag;
            int ammoInBag = AmmoCollected[weapon.Data.AmmoType];
            int newAmmoInBag = ammoInBag - ammoNeeded;

            if (newAmmoInBag < 0)
            {
                ammoNeeded = newAmmoInBag + ammoNeeded;
                newAmmoInBag = 0;
            }

            AmmoCollected[weapon.Data.AmmoType] = newAmmoInBag;

            ammoNeeded += weapon.AmmoInMag;
            weapon.SetAmmoInMag(ammoNeeded);

        }
    }
}
 
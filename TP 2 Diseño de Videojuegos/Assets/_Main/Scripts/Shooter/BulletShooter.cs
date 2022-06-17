using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class BulletShooter : BulletSpawner, IObservable
    {
        //[Header("Shooter Values")]
        public bool CanShoot { get; private set; }
        public bool IsShooting => _currentShootCD > 0;
        public bool IsReloading => _currentReloadCD > 0;
        public List<IObserver> Subscribers => _subscribers;

        private WeaponController _weapon;
        private AmmoController _ammo;

        private float _currentShootCD;
        private float _currentReloadCD;

        private List<Bullet> _bulletsInWorld = new List<Bullet>();
        private List<IObserver> _subscribers = new List<IObserver>();

        private void Awake()
        {
            _weapon = GetComponent<WeaponController>();
            _ammo = GetComponent<AmmoController>();
        }
        private void Start()
        {
            EnableShooting(true);
            UpdateAmmoHud();
            UpdateWeaponHud();
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

        public void EnableShooting(bool enable)
        {
            CanShoot = enable;
        }

        public void DoShoot()
        {
            if (_weapon.GetCurrentAmmoInMag() == 0)
            {
                DoReload();
                return;
            }

            if (!CanShoot || IsShooting || IsReloading || _currentShootCD > 0) return;

            _bulletsInWorld.Add(CreateBullet(_weapon.CurrentWeapon.Data.BulletType));
            _weapon.ShootAmmo();
            UpdateAmmoHud();
            ResetShootCoolDown();

        }

        public void DoReload()
        {
            if (_weapon.GetCurrentAmmoInMag() == _weapon.CurrentWeapon.Data.MagazineSize || IsReloading || IsShooting) return;

            if (!_ammo.ReloadAmmo(_weapon)) return;

            NotifyAll("RELOAD");
            ResetReloadCoolDown();
        }

        public void DoCycleWeapons()
        {
            _weapon.CycleWeapons();
            UpdateAmmoHud();
            UpdateWeaponHud();
        }

        private void UpdateAmmoHud() => NotifyAll("AMMOUPDATE", _weapon.GetCurrentAmmoInMag(), _ammo.GetAmmo(_weapon));
        private void UpdateWeaponHud() => NotifyAll("WEAPONUPDATE", _weapon.CurrentWeapon.Data.WeaponName);

        private void ResetShootCoolDown() => _currentShootCD = _weapon.CurrentWeapon.Data.Cadence;
        private void ResetReloadCoolDown() => _currentReloadCD = _weapon.CurrentWeapon.Data.ReloadTime;

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
}

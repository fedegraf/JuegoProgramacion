using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class BulletShooterV2 : BulletSpawner
    {
        //[Header("Shooter Values")]
        public bool CanShoot { get; private set; }
        public bool IsShooting => _currentShootCD > 0;
        public bool IsReloading => _currentReloadCD > 0;

        private WeaponController _weapon;
        private AmmoController _ammo;

        private float _currentShootCD;
        private float _currentReloadCD;

        private List<Bullet> _bulletsInWorld = new List<Bullet>();

        private void Awake()
        {
            _weapon = GetComponent<WeaponController>();
            _ammo = GetComponent<AmmoController>();
        }

        private void Update()
        {
            if(_currentShootCD > 0)
                _currentShootCD -= Time.deltaTime;
            if(_currentReloadCD > 0)
                _currentReloadCD -= Time.deltaTime;
        }

        private void Start()
        {
            EnableShooting(true);
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
            }

            if (!CanShoot || IsShooting || IsReloading || _currentShootCD > 0) return;

            _bulletsInWorld.Add(CreateBullet(_weapon.CurrentWeapon.BulletType));
            _weapon.ShootAmmo();
            ResetShootCoolDown();

        }

        public void DoReload()
        {
            if (_weapon.GetCurrentAmmoInMag() == _weapon.CurrentWeapon.MagazineSize || IsReloading || IsShooting) return;

            _ammo.ReloadAmmo(_weapon);
            ResetReloadCoolDown();
        }

        private void ResetShootCoolDown() => _currentShootCD = _weapon.CurrentWeapon.Cadence;
        private void ResetReloadCoolDown() => _currentReloadCD = _weapon.CurrentWeapon.ReloadTime;


    }
}

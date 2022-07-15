using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public class TurretShootingScript : BulletSpawner, IObservable
    {
        [Header("Weapon Values")] [SerializeField]
        private WeaponTypeSO[] weaponsType;

        [SerializeField] private GameObject weaponGO;

        private float _currentShootCD;
        private float _currentReloadCD;
        private List<IObserver> _subscribers = new List<IObserver>();
        private List<Weapon> _weaponsList = new List<Weapon>();
        private List<BulletBase> _bulletsInWorld = new List<BulletBase>();
        [SerializeField] public float ShootingCooldown = 1.75f;
        public float CurrentCooldown = 0f;


        public Weapon CurrentWeapon { get; private set; }
        public AmmoController Ammo { get; private set; }
        public List<Weapon> WeaponsList => _weaponsList;
        public bool CanShoot { get; private set; }
        public bool IsShooting => _currentShootCD > 0;
        public bool IsReloading => _currentReloadCD > 0;
        public List<IObserver> Subscribers => _subscribers;

        public void Suscribe(IObserver observer)
        {
        }

        public void Unsuscribe(IObserver observer)
        {
        }

        public void NotifyAll(string message, params object[] args)
        {
        }

        //Test 
        [Header("Test Values")] [SerializeField]
        AmmoTypeSO[] ammoTypeToAdd;

        [SerializeField] int[] ammoAmmountToAdd;
        private TakeAmmoCommand _takeAmmoCommand;
        private SoundManager _sounds;


        private void Awake()
        {
            Ammo = new AmmoController();
            _sounds = GetComponent<SoundManager>();
        }

        private void Start()
        {
            SetWeaponsList();
            AddAmmo();
        }

        private void Update()
        {
            CurrentCooldown += Time.deltaTime;
            if (gameObject.GetComponent<TurretAimingScript>().isTargetInRange)
            {
                if (CurrentCooldown >= ShootingCooldown)
                {
                    DoShoot();
                    CurrentCooldown = 0;
                }
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
        
        public void AddWeapon(Weapon newWeapon)
        {
            if (_weaponsList.Contains(newWeapon))
            {
                Debug.Log("You already own this weapon");
                return;
            }

            _weaponsList.Add(newWeapon);
            SetWeapon(_weaponsList.Count-1);
        }
        private void AddAmmo()
        {
            for (int i = 0; i < ammoTypeToAdd.Length; i++)
            {
                _takeAmmoCommand = new TakeAmmoCommand(Ammo, ammoTypeToAdd[i], ammoAmmountToAdd[i]);
                _takeAmmoCommand.Do();
            }
        }

        private void SetWeapon(int index)
        {
            CurrentWeapon = _weaponsList[index];
            if (CurrentWeapon == null) return;

            var weaponSMR = weaponGO.GetComponent<SkinnedMeshRenderer>();
            if (!weaponSMR) return;
            weaponSMR.sharedMesh = CurrentWeapon.Data.Mesh;
        }

        private void ShootSound()
        {
            _sounds.PlaySound("Shoot");
        }


        private void ResetShootCoolDown() => _currentShootCD = CurrentWeapon.Data.Cadence;
        private void ResetReloadCoolDown() => _currentReloadCD = CurrentWeapon.Data.ReloadTime;

        public void DoShoot()
        {
            _bulletsInWorld.Add(CreateBullet(CurrentWeapon.Shoot()));
            ShootSound();
            ResetShootCoolDown();
        }
    }
}
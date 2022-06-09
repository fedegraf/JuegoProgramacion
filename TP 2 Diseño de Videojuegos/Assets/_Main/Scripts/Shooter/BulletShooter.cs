using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : BulletSpawner, IObservable
{
    [Header("Shooter Values")]
    [SerializeField] private BulletTypeSO bulletType;
    public bool CanShoot { get; private set; }
    public bool IsShooting { get; private set; }
    public int CurrentAmmo { get; private set; }
    public bool IsReloading { get; private set; }
    [SerializeField] private int maxAmmo = 15;

    [Range(0, 2f)]
    [SerializeField] private float maxShootCoolDown = 1f;
    private float _shootCoolDown;

    [Range(0, 2f)]
    [SerializeField] private float reloadTime = 1f;
   

    private List<Bullet> _bulletsInWorld = new List<Bullet>();

    public List<IObserver> Subscribers => _subscribers;
    private List<IObserver> _subscribers = new List<IObserver>();

    private void Awake()
    {

    }

    private void Start()
    {
        CanShoot = true;
        CurrentAmmo = maxAmmo;

        UpdateAmmoHud();
    }

    private void Update()
    {
        if(_shootCoolDown > 0)
            _shootCoolDown -= Time.deltaTime;
    }

    public void DoShoot()
    {
        if (CurrentAmmo < 1 || !CanShoot || IsShooting || IsReloading || !bulletType || _shootCoolDown > 0) return;

        StartCoroutine(ShootWeapon());
    }

    public void DoReloadAmmo()
    {
        if (CurrentAmmo == maxAmmo || IsReloading || !bulletType || IsShooting) return;

        StartCoroutine(ReloadAmmo());
    }

    public void EnableShooting(bool enable)
    {
        CanShoot = enable;
    }

    private void ResetShooCoolDown() => _shootCoolDown = maxShootCoolDown;

    private IEnumerator ShootWeapon()
    {
        IsShooting = true;

        _bulletsInWorld.Add(CreateBullet(bulletType));
        ResetShooCoolDown();
        CurrentAmmo--;

        UpdateAmmoHud();

        yield return new WaitForSeconds(0.5f);

        IsShooting = false;

        yield return null;
    }

    private IEnumerator ReloadAmmo()
    {
        NotifyAll("RELOAD");

        IsReloading = true;

        yield return new WaitForSeconds(reloadTime);

        CurrentAmmo = maxAmmo;
        IsReloading = false;

        UpdateAmmoHud();
        yield return null;
    }

    private void UpdateAmmoHud() => NotifyAll("AMMOUPDATE", CurrentAmmo, maxAmmo);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponInfo : MonoBehaviour, IObserver
{
    [SerializeField] private Text currentAmmoText;

    private void Start()
    {
        GetComponent<BulletShooter>().Suscribe(this);
    }

    public void OnNotify(string message, params object[] args)
    {
        if (message == "AMMOUPDATE")
        {
            UpdateAmmoText((int)args[0], (int)args[1]);
        }
        else if (message == "RELOAD")
        {
            currentAmmoText.text = "RELOADING";
        }
    }

    private void UpdateAmmoText(int currentAmmo, int maxAmmo)
    {
        currentAmmoText.text = $"Ammo: {currentAmmo}/{maxAmmo}";
    }
}

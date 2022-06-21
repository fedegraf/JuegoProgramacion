using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    public class PlayerWeaponInfo : MonoBehaviour, IObserver
    {
        [SerializeField] private Text currentAmmoText;
        [SerializeField] private Text currentWeaponText;

        private void Start()
        {
            GetComponent<WeaponController>().Suscribe(this);
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
            else if (message == "WEAPONUPDATE")
            {
                UpdateCurrentWeaponText((string)args[0]);
            }
        }

        private void UpdateAmmoText(int currentAmmo, int maxAmmo)
        {
            currentAmmoText.text = $"Ammo: {currentAmmo}/{maxAmmo}";
        }

        private void UpdateCurrentWeaponText(string weaponName)
        {
            currentWeaponText.text = $"Weapon: {weaponName}";
        }
    }
}

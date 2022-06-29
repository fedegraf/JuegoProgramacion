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
                UpdateAmmoText((string)args[0]);
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

        private void UpdateAmmoText(string weaponInfo)
        {
            currentAmmoText.text = $"Ammo: {weaponInfo}";
        }

        private void UpdateCurrentWeaponText(string weaponName)
        {
            currentWeaponText.text = $"Weapon: {weaponName}";
        }
    }
}

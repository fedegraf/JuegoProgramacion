using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Medikit : Item, IUsableItem
    {
        [SerializeField] private int healAmmount;
        private TakeHealCommand _healCommand;

        public bool Use(GameObject user)
        {
            if (!user.TryGetComponent<IDamagable>(out var damagable)) return false;

            _healCommand = new TakeHealCommand(damagable, healAmmount);

            _healCommand.Do();
            gameObject.SetActive(false);
            return true;

        }
    }
}


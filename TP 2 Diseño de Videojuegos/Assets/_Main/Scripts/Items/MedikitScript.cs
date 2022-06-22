using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class MedikitScript : BaseItem, IInteractable
    {
        [SerializeField] private int healAmmount;
        private TakeHealCommand _command;

        public string Interact(GameObject user)
        {
            if (!user.TryGetComponent<IDamagable>(out var damagable)) return "You can't heal right now";

            if (damagable.CurrentHealth >= damagable.MaxHealth)
            {
                return "You Have Max Health";
            }


            _command = new TakeHealCommand(damagable, healAmmount);
            _command.Do();
            gameObject.SetActive(false);
            return $"You healed {healAmmount} PS";
        }

        public void SetHealAmmount(int heal)
        {
            healAmmount = heal;
        }
    }
}


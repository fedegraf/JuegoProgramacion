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

        public object[] Test(GameObject user)
        {
            object[] values = new object[5];

            if (!user.TryGetComponent<IDamagable>(out var damagable))
            {
                values[0] = "You can't heal right now";
                values[1] = false;
                return values;
            }

            if (damagable.CurrentHealth >= damagable.MaxHealth)
            {
                values[0] = "You Have Max Health";
                values[1] = false;
                return values;
            }


            _command = new TakeHealCommand(damagable, healAmmount);
            _command.Do();
            gameObject.SetActive(false);
           
            values[0] = $"You got healed";
            values[1] = true;
            return values;
        }
    }
}


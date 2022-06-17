using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class MediKit : BaseItem, IInteractable
    {
        [SerializeField] private int healAmmount;
        private TakeHealCommand _command;

        public bool Interact(GameObject user)
        {
            if (!user.TryGetComponent<IDamagable>(out var damagable)) return false;

            if (damagable.CurrentHealth >= damagable.MaxHealth)
            {
                Debug.Log("You have max health");
                return false;
            }
                

            _command = new TakeHealCommand(damagable, healAmmount);
            _command.Do();
            gameObject.SetActive(false);
            return true;
        }
    }
}

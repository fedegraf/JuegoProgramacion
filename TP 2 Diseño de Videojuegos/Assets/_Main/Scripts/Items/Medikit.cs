using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Medikit : Item, IUsableItem
    {
        [SerializeField] private int heal;
        private TakeHealCommand _healCommand;

        public bool Use(GameObject user)
        {
            if (!user.TryGetComponent<IDamagable>(out var damagable)) return false;

            _healCommand = new TakeHealCommand(damagable, heal);

            if (_healCommand.Do())
            {
                gameObject.SetActive(false);
                return true;
            }
            return false;
                
            
        }
    }
}


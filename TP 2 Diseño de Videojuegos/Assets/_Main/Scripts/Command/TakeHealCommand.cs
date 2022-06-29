using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHealCommand : ICommand
{
    private IDamagable _victim;
    private float _heal;

    public TakeHealCommand(IDamagable victim, float heal)
    {
        _victim = victim;
        _heal = heal;
    }

    public void Do()
    {
        if (_victim.CurrentHealth == _victim.MaxHealth) return;

        _victim.TakeHeal(_heal);
    }
}

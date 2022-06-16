using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHealCommand
{
    private IDamagable _victim;
    private float _heal;

    public TakeHealCommand(IDamagable victim, float heal)
    {
        _victim = victim;
        _heal = heal;
    }

    public bool Do()
    {
        if (_victim.CurrentHealth == _victim.MaxHealth) return false;

        _victim.TakeHeal(_heal);
        return true;
    }
}

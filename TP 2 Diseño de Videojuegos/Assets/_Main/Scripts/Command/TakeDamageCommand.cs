using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageCommand : ICommand
{
    private IDamagable _victim;
    private float _damage;

    public TakeDamageCommand(IDamagable victim, float damage)
    {
        _victim = victim;
        _damage = damage;
    }

    public void Do()
    {
        _victim.TakeDamage(_damage);
    }
}

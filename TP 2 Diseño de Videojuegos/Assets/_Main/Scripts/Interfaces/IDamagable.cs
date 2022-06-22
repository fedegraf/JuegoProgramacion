using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public float MaxHealth { get; }
    public float CurrentHealth { get; }
    public bool IsAlive { get; }
    public bool IsVulnearble { get; }

    public void TakeDamage(float damage);
    public void TakeHeal(float heal);
    public void SetVulnerable(bool vulnerable);
    public void Die();
}

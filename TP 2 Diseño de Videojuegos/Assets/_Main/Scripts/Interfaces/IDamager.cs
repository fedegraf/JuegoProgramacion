using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamager
{
    public float Damage { get; }
    public void SetDamage(float newDamage);
}

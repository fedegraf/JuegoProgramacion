using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDamagable : Damagable
{
    public override void Die()
    {
        gameObject.layer = LayerMask.NameToLayer("DeadEntity");
        gameObject.tag = "DeadEntity";
        GetComponent<SoundManager>().PlaySound("Dead");
//        _sound.PlaySound("Dead");
        OnDie?.Invoke();
     Destroy(gameObject);   
    }
}

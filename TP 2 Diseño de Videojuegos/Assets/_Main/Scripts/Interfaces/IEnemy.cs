using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public GameObject Enemy { get; }
    public Character Character { get; }
    public bool CanMove { get; }
    public void SetPlayerDamageCommand(Damagable player);
    public void EnableMovement(bool enable);
    public void Movement(Vector2 direction);
    public void DoAttack();
}

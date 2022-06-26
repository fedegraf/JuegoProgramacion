using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public GameObject Enemy { get; }
    public Character Character { get; }
    public bool CanMove { get; }
    public bool IsMoving { get; }
    public bool IsFollowingPlayer { get; }
    public bool IsAttacking { get; }
    public bool IsDead { get; }
    public bool IsStuned { get; }
    public void SetPlayerDamageCommand(Damagable player);
    public void EnableMovement(bool enable);
    public void Stunt();
    public void Movement(Vector2 direction);
    public void SetIsMoving(bool isMoving);
    public void SetIsFollowing(bool isFollowingPlayer);
    public void SetIsAttacking(bool isAttacking);
    public void SetIsDead(bool isDead);
    public void DoAttack();
}

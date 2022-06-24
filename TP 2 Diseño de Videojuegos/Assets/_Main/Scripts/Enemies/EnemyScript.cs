using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour, IEnemy
{
    public GameObject Enemy => gameObject;
    public bool CanMove {get; private set;}
    public Character Character { get; private set; }
    public TakeDamageCommand PlayerDamageCommand { get; private set; }
    public Damagable Damagable { get; private set; }

    public bool IsAttacking { get; private set; }

    public bool IsMoving { get; private set; }
    public bool IsFollowingPlayer { get; private set; }

    public bool IsDead { get; private set; }

    public bool IsStuned { get; private set; }

    [SerializeField] private float damage;

    private void Awake()
    {
        Character = GetComponent<Character>();
        Damagable = GetComponent<Damagable>();
        Damagable.OnDie += OnDieHandler;
        SetIsDead(false);
    }

    private void Start()
    {
        EnableMovement(true);
    }

    private void OnDieHandler()
    {
        SetIsDead(true);
        gameObject.layer = 13;
    }

    public void SetPlayerDamageCommand(Damagable player)
    {
        PlayerDamageCommand = new TakeDamageCommand(player, damage);
    }

    public void EnableMovement(bool enable)
    {
        CanMove = enable;
        if (!enable)
        {
            SetIsFollowing(false);
        }
    }

    public void Movement(Vector2 direction)
    {
        Character.DoWalking(direction, false); 
    }

    public void DoAttack()
    {
        PlayerDamageCommand.Do();
    }

    public void SetIsAttacking(bool isAttacking)
    {
        IsAttacking = isAttacking;
    }

    public void SetIsMoving(bool isMoving)
    {
        IsMoving = isMoving;
    }

    public void SetIsFollowing(bool isFollowingPlayer)
    {
        IsFollowingPlayer = isFollowingPlayer;
    }

    public void SetIsDead(bool isDead)
    {
        IsDead = isDead;
    }

    public void Stunt()
    {
        StartCoroutine(StuntTime());
    }

    private IEnumerator StuntTime()
    {
        var time = 3;
        IsStuned = true;
        yield return new WaitForSeconds(time);
        IsStuned = false;
    }

}



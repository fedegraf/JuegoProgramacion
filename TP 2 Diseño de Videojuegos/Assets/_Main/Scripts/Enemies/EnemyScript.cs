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

    [SerializeField] private float damage;

    private void Awake()
    {
        Character = GetComponent<Character>();
    }

    private void Start()
    {
        EnableMovement(true);
    }

    public void SetPlayerDamageCommand(Damagable player)
    {
        PlayerDamageCommand = new TakeDamageCommand(player, damage);
    }

    public void EnableMovement(bool enable)
    {
        CanMove = enable;
    }

    public void Movement(Vector2 direction)
    {
        Character.DoWalking(direction, false); 
    }

    public void DoAttack()
    {
        PlayerDamageCommand.Do();
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    private NavMeshAgent _agent;
    private GameObject _player;
    private IEnemy _enemy;

    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;

    //Patrolling Values
    private Vector3 walkPoint;
    private bool _walkPointSet;
    [SerializeField] private float walkPointRange;

    //Attacking Values
    [SerializeField] private int damage;
    [SerializeField] private float timeBetweenAttacks;
    private bool alreadyAttacked;

    //States Values
    [SerializeField] private float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        _player = GameObject.Find("Player");
        _agent = GetComponent<NavMeshAgent>();
        _enemy = GetComponent<IEnemy>();
    }

    private void Start()
    {
        _agent.speed = _enemy.Character.Stats.WalkSpeed;
        _enemy.SetPlayerDamageCommand(_player.GetComponent<Damagable>());
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!_enemy.CanMove) return;

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange) AttackPlayer();
    }

    private void Patrolling()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet)
            _agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            _walkPointSet = false;

        //_enemy.Movement();
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            _walkPointSet = true;
    }

    private void ChasePlayer()
    {
        _agent.SetDestination(_player.transform.position);
    }

    private void AttackPlayer()
    {
        _agent.SetDestination(transform.position);

        transform.LookAt(_player.transform.position);

        if (!alreadyAttacked)
        {
            _enemy.DoAttack();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmos()
    {
        //Walking Range
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);

        //Sight Range
        Gizmos.color = playerInSightRange ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        //Attack Range
        Gizmos.color = playerInAttackRange ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour, IEnemy
{
    public GameObject Enemy => gameObject;
    public bool CanMove => _canMove;

    private bool _canMove;
    private Character _character;
    private GameObject _player;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
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
        _character = GetComponent<Character>();
        _player = GameObject.Find("Player").gameObject;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = _character.Stats.WalkSpeed;
    }

    private void Start()
    {

    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange) AttackPlayer();

    }

    private void Patrolling()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            _walkPointSet = false;
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
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            _player.GetComponent<Damagable>()?.TakeDamage(damage);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void EnableMovement(bool enable)
    {
        _canMove = enable;
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



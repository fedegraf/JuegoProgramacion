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
        private bool _hasSeenPlayer;

    //States Values
    [SerializeField] private float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
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
        if (_enemy.IsDead || _enemy.IsStuned)
        {
            playerInSightRange = false;
            playerInAttackRange = false;
            _walkPointSet = false;
            _agent.SetDestination(transform.position);
            _agent.speed = 0;
            return;
        }
        
        if(_agent.speed == 0)
            _agent.speed = _enemy.Character.Stats.WalkSpeed;

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            _enemy.SetIsMoving(true);
            if (_enemy.IsFollowingPlayer) _enemy.SetIsFollowing(false);

            Patrolling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            _enemy.SetIsFollowing(true);
            if (_enemy.IsAttacking) _enemy.SetIsAttacking(false);
            ChasePlayer();
        }
        if (playerInAttackRange)
        {
            _enemy.SetIsAttacking(true);
            if (_enemy.IsFollowingPlayer)
            {
                _enemy.SetIsFollowing(false);
                _enemy.SetIsMoving(false);
            }
            
            StartCoroutine(AttackPlayer());
        }
    }

    private void Patrolling()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet)
            _agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        if (distanceToWalkPoint.magnitude < 1f)
            _walkPointSet = false;

        _enemy.SetIsMoving(true);

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

    private IEnumerator AttackPlayer()
    {
        //wait for the animation begin
        yield return new WaitForSeconds(0.5f);
        _agent.speed = 0;

        var lastRotation = transform.rotation;
        transform.LookAt(_player.transform.position);
        transform.rotation = new Quaternion(lastRotation.x, transform.rotation.y, lastRotation.z, lastRotation.w);

        if (!alreadyAttacked)
        {
            if(playerInAttackRange)
                _enemy.DoAttack();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        _enemy.SetIsAttacking(false);
    }

    private void OnDrawGizmosSelected()
    {
        //Walking Range
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);

        //Sight Range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        //Attack Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

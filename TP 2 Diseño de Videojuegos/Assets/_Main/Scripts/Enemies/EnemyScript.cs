using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IEnemy
{
    public GameObject Enemy => gameObject;
    public EnemyTypeSO Stats => stats;
    public bool CanMove => _canMove;

    private bool _canMove;
    private Character _character;
    private PlayerInputs _player;
    private float _distanceToPlayer;

    [SerializeField] private EnemyTypeSO stats;

    private void Start()
    {
        _character = GetComponent<Character>();
        _player = FindObjectOfType<PlayerInputs>();
    }

    private void Update()
    {
    
    }

    private void Movement()
    {
        _character.DoWalking(Vector2.zero);
    }


    private bool IsPlayerInRange()
    {
        if (_player == null) return false;

        return Vector3.Distance(transform.position, _player.transform.position) <= Stats.AttackRadius;
    }

    public void EnableMovement(bool enable)
    {
        _canMove = enable;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsPlayerInRange() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, Stats.AttackRadius);
    }
}



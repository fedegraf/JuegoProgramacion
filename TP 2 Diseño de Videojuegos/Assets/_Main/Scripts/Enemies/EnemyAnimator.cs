using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;
        private IEnemy _enemy;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _enemy = GetComponent<IEnemy>();
        }

        private void FixedUpdate()
        {
            if (_enemy != null)
            {
                Movements();
                Attacking();
            }
        }

        private void Movements()
        {
            _animator.SetBool("IsWalking", _enemy.IsMoving);
            _animator.SetBool("IsChasing", _enemy.IsFollowingPlayer);
        }

        private void Attacking()
        {
            if (_enemy.IsAttacking)
                _animator.SetTrigger("Attack");
        }
    }
}

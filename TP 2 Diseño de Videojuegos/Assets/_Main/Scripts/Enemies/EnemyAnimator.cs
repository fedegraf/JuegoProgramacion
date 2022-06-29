using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator; 
        private IEnemy _enemy;


        private void Awake()
        {
            if(animator == null)
                animator = GetComponent<Animator>();
            _enemy = GetComponent<IEnemy>();
        }

        private void FixedUpdate()
        {
            if (_enemy != null)
            {
                Movements();
                Attacking();
                Death();
            }
        }

        private void Movements()
        {
            animator.SetBool("IsWalking", _enemy.IsMoving);
            animator.SetBool("IsChasing", _enemy.IsFollowingPlayer);
        }

        private void Attacking()
        {
            if (_enemy.IsAttacking)
                animator.SetTrigger("Attack");
        }


        private void Death()
        {
            if(_enemy.IsDead)
            animator.SetTrigger("Death");
        }
    }
}

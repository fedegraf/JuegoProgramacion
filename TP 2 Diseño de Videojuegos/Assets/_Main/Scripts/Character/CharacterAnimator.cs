using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rb;
    private Character _character;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _character = GetComponent<Character>();
    }

    private void FixedUpdate()
    {
        if (_rb)
        {
            MovementsValues();
        }
        if (_character.Shooter)
        {
            ShootingValues();
        }
    }

    private void MovementsValues()
    {
        _animator.SetFloat("Movement", _rb.velocity.magnitude);

        var direction = new Vector2(_rb.velocity.normalized.x, _rb.velocity.normalized.z);

        _animator.SetFloat("InputH", direction.x);
        _animator.SetFloat("InputV", direction.y);

    }

    private void ShootingValues()
    {
        _animator.SetBool("IsShooting", _character.Shooter.IsShooting);
        _animator.SetBool("IsReloading", _character.Shooter.IsReloading);
    }
}

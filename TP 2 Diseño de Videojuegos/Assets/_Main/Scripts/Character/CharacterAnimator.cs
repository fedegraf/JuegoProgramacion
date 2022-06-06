using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMovementValue(float speed)
    {
        _animator.SetFloat("Movement", speed);
    }

    public void SetInputsValue(Vector2 inputs)
    {
        _animator.SetFloat("InputH", inputs.x);
        _animator.SetFloat("InputV", inputs.y);
    }

    public void SetShooting(bool isShooting)
    {
        _animator.SetBool("IsShooting", isShooting);
    }

    public void SetReloading(bool isReloading)
    {
        _animator.SetBool("IsReloading", isReloading);
    }
}

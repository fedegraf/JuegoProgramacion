using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Movement _movement;
    private CharacterAnimator _characterAnimator;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    private void Update()
    {
        _characterAnimator.SetMovementValue(_movement.BodySpeed);
    }

    public void DoWalking(Vector2 direction)
    {
        _movement.Walking(direction);
        _characterAnimator.SetInputsValue(direction);
    }

    public void DoRotation(Vector2 rotation)
    {
        _movement.Rotation(rotation);
    }
}
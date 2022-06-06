using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _rBody;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float rotationSpeed;
    public float BodySpeed => _rBody.velocity.magnitude;

    private void Awake()
    {
        _rBody = GetComponent<Rigidbody>();
    }

    public void Walking(Vector2 inputDirection)
    {
        Vector3 direction = new Vector3(-inputDirection.x, 0, inputDirection.y);

        _rBody.AddForce(direction * walkingSpeed, ForceMode.Force);
    }

    public void Rotation(Vector2 inputRotation)
    {
        transform.Rotate(Vector3.up, inputRotation.x * rotationSpeed);
    }
}

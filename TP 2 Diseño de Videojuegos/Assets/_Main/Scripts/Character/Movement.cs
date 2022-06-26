using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _rBody;
    private float _walkingSpeed = 5;
    private float _runningSpeed = 5;
    private float _rotationSpeed = 10;
    public bool IsRunning { get; private set; }
    public float CurrentSpeed => IsRunning ? _runningSpeed : _walkingSpeed;
    public float BodySpeed => _rBody.velocity.magnitude;

    private void Awake()
    {
        _rBody = GetComponent<Rigidbody>();
    }

    public void SetValues(float walkSpeed, float runningSpeed, float rotationSpeed)
    {
        _walkingSpeed = walkSpeed;
        _runningSpeed = runningSpeed;
        _rotationSpeed = rotationSpeed;
    }

    public void Walking(Vector2 inputDirection, bool isRunning)
    {
        this.IsRunning = isRunning;

        Vector3 direction = new Vector3(-inputDirection.x, 0, inputDirection.y);

        _rBody.AddForce(direction * CurrentSpeed, ForceMode.Force);
    }

    public void Rotation(Vector2 inputRotation)
    {
        transform.Rotate(Vector3.up, inputRotation.x * (_rotationSpeed * Time.deltaTime));
    }
}

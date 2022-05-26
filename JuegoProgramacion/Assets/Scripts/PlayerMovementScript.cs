using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] private float speed = 1000f;
    public Vector3 FORCECONST = new Vector3(0,1,0);
    private bool isGrounded;
    [SerializeField] private float playerHeight = 2;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject aimObject;
    [SerializeField] private Rigidbody rb_Player;
    [SerializeField] private float groundDrag = 5f;

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);
        rb_Player.drag = isGrounded ? groundDrag : 0;

    }

    public void Jump()
    {

        if (isGrounded)
        {
            rb_Player.AddForce(FORCECONST, ForceMode.Impulse);
        }
    }

    public void MoveForward()
    {
        rb_Player.AddForce(transform.forward * speed, ForceMode.Force);
    }
    public void MoveBack()
    {
        rb_Player.AddForce(-transform.forward * speed, ForceMode.Force);
    }
    public void MoveLeft()
    {
        rb_Player.AddForce(-transform.right * speed, ForceMode.Force);
    }
    public void MoveRight()
    { 
        rb_Player.AddForce(transform.right * speed, ForceMode.Force);
    }

    public void Shoot()
    {
        GameObject clone = Instantiate(bulletPrefab, aimObject.transform.position, aimObject.transform.rotation);
        clone.GetComponent<BulletScript>().Owner = "Player";

    }
}
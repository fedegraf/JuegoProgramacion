using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] private float speed = 1000f;
    [SerializeField] private float playerHeight = 2;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject aimObject;
    [SerializeField] private Rigidbody rb_Player;
    [SerializeField] private float groundDrag = 5f;

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
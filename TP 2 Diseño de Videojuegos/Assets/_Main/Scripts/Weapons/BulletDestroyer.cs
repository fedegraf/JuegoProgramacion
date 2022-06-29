using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Col Detected");

        if (!collision.gameObject.CompareTag("bullet")) return;

        Destroy(collision.gameObject);
    }
}

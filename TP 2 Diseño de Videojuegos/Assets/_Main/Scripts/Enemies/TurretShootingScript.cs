using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class TurretShootingScript : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Head;
    [SerializeField] private GameObject Barrell;
    [SerializeField] public float ShootingCooldown = 1.75f;
    public float CurrentCooldown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentCooldown += Time.deltaTime;
        if (gameObject.GetComponent<TurretAimingScript>().isTargetInRange)
        {
            if (CurrentCooldown >= ShootingCooldown)
            {
                Shoot();
                CurrentCooldown = 0;
            }   
        }
    }

    void Shoot()
    {
        Debug.Log("Shoot");
        GameObject clone = Instantiate(Bullet, Barrell.transform.position, Head.transform.rotation);
    }
}
 
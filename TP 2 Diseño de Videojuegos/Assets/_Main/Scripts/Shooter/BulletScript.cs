using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    bool isActive;
    [SerializeField] private GameObject Bullet;
    public String Owner;

    void Start()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.position += transform.forward * ScriptableObjects.BulletSpeed * Time.deltaTime;
        }
    }

    public void Activate()
    {
        isActive = true;        
    }

    public void Deactivate()
    {
        isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(Owner) && !other.gameObject.CompareTag("Untagged"))
        {
  /*          IInteractive interact = (IInteractive) other.GetComponent(typeof(IInteractive));
            if (interact != null)
            {
                interact.GetHitByBullets();
            }
                  gameObject.SetActive(false);
    */    }
    }
}

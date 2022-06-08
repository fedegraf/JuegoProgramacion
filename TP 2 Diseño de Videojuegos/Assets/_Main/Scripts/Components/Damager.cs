using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private bool isDestroyable;
    private IDamager iDamager;

    private void Start()
    {
        iDamager = GetComponent<IDamager>();
        if (iDamager != null) _damage = iDamager.Damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Damagable damagable = collision.gameObject.GetComponent<Damagable>();
        if (!damagable) return;

        damagable.TakeDamage(_damage);
        if (isDestroyable) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Damagable damagable = other.gameObject.GetComponent<Damagable>();
        if (!damagable) return;

        damagable.TakeDamage(_damage);
        if (isDestroyable) Destroy(gameObject);
    }
}

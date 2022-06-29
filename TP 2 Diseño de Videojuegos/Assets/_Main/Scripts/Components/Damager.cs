using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour, IDamager
{
    [SerializeField] private float _damage;
    [SerializeField] private bool isDestroyable;
    public float Damage => _damage;
    public void SetDamage(float newDamage)
    {
        _damage = newDamage;
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

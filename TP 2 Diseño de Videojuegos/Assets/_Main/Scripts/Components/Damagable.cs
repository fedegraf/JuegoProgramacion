using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour, IDamagable
{
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    public bool IsAlive => _isAlive;

    private float _currentHealth; 
    private bool _isAlive;

    [SerializeField] private int _maxHealth;
    private void Awake()
    {
        _isAlive = true;
        _currentHealth = MaxHealth;
    }

    public void Die()
    {
        _isAlive = false;
        Debug.Log($"{gameObject.name} Dead");
    }

    public void TakeDamage(float damage)
    {
        if (!IsAlive) return;

        _currentHealth -= damage;
        if (_currentHealth < 0) _currentHealth = 0;

        if (_currentHealth == 0) Die();
        else Debug.Log($"Current {gameObject.name} Health:{CurrentHealth}");
    }

    public void TakeHeal(float heal)
    {
        if(!IsAlive) return;

        _currentHealth += heal;
        if (_currentHealth > MaxHealth) _currentHealth = MaxHealth;
    }
}

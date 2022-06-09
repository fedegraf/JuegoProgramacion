using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour, IDamagable, IObservable
{
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    public bool IsAlive => _isAlive;
    public List<IObserver> Subscribers => _subscribers;
    private List<IObserver> _subscribers = new List<IObserver>();

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

        NotifyAll("TAKE_DAMAGE");
    }

    public void TakeHeal(float heal)
    {
        if(!IsAlive) return;

        _currentHealth += heal;
        if (_currentHealth > MaxHealth) _currentHealth = MaxHealth;
        NotifyAll("TAKE_DAMAGE");
    }

    public void Suscribe(IObserver observer)
    {
        if (_subscribers.Contains(observer)) return;
        _subscribers.Add(observer);
    }

    public void Unsuscribe(IObserver observer)
    {
        if (!_subscribers.Contains(observer)) return;
        _subscribers.Remove(observer);
    }

    public void NotifyAll(string message, params object[] args)
    {
        if (_subscribers.Count < 1) return;

        foreach (var suscriber in _subscribers)
        {
            suscriber.OnNotify(message, args);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour, IDamagable, IObservable
{
    [SerializeField] private bool isVulnerable = true;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    public bool IsAlive => _isAlive;
    public List<IObserver> Subscribers => _subscribers;

    public bool IsVulnearble => isVulnerable;

    private List<IObserver> _subscribers = new List<IObserver>();
    private float _currentHealth; 
    private bool _isAlive;

    private DieCommand dieCommand;
    private SoundManager _sound;

    [SerializeField] private int _maxHealth;
    [SerializeField] private GameObject bloodEffect;

    public UnityAction OnDie;

    private void Awake()
    {
        _isAlive = true;
        _currentHealth = MaxHealth;
        dieCommand = new DieCommand(this, gameObject.tag);
        _sound = GetComponent<SoundManager>();
    }

    private void SpawnBlood()
    {
        if (!bloodEffect) return;

        Instantiate(bloodEffect, transform);
        _sound.PlaySound("Damage");
    }

    public void Die()
    {
        _isAlive = false;
        gameObject.layer = LayerMask.NameToLayer("DeadEntity");
        _sound.PlaySound("Dead");
        OnDie?.Invoke();
    }

    public void TakeDamage(float damage)
    {   
        SpawnBlood();

        if (!IsAlive || !IsVulnearble) return;

        _currentHealth -= damage;
        if (_currentHealth < 0) _currentHealth = 0;

        if (_currentHealth == 0) dieCommand.Do();

        NotifyAll("TAKE_DAMAGE");
    }

    public void TakeHeal(float heal)
    {
        if(!IsAlive) return;

        _currentHealth += heal;
        if (_currentHealth > MaxHealth) _currentHealth = MaxHealth;
        _sound.PlaySound("Heal");
        NotifyAll("TAKE_DAMAGE");
    }
    public void SetVulnerable(bool vulnerable)
    {
        isVulnerable = vulnerable;
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

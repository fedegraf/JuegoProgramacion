using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IObserver
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;
    [SerializeField] private Color color3;

    private Damagable _damagable;

    private void Start()
    {
        _damagable = GetComponent<Damagable>();
        _damagable.Suscribe(this);
        _damagable.OnDie += OnDeadHandler;
        SetHealthColors();
    }

    public void OnNotify(string message, params object[] args)
    {
        if (message != "TAKE_DAMAGE") return;

        _healthBar.fillAmount = (float)_damagable.CurrentHealth / _damagable.MaxHealth;

        SetHealthColors();
    }

    private void SetHealthColors()
    {
        if (_damagable.CurrentHealth >= 75) SetColor(color1);
        else if (_damagable.CurrentHealth < 75 && _damagable.CurrentHealth >= 25) SetColor(color2);
        else if (_damagable.CurrentHealth < 25) SetColor(color3);
    }

    private void SetColor(Color newCOlor)
    {
        _healthBar.color = newCOlor;
    }

    private void OnDeadHandler()
    {
        _healthBar.gameObject.transform.parent.gameObject.SetActive(false);
    }
}

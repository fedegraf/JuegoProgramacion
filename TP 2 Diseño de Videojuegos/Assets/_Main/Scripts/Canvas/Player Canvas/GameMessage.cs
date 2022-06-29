using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMessage : MonoBehaviour, IObserver
{
    [Header("Message Values")]
    [SerializeField] private Text messageText;
    [SerializeField] private float maxTime;
    [Header("Observables")]
    [SerializeField] private GameObject player;
    [SerializeField] private Items.ItemChecker itemChecker;

    private float _currentTime;
    private float _alpha;

    private void Awake()
    {
        player.GetComponent<Items.ItemLooter>()?.Suscribe(this);
        player.GetComponent<Items.ItemInteracter>()?.Suscribe(this);
        player.GetComponent<Skills.SkillController>()?.Suscribe(this);
        itemChecker?.Suscribe(this);
    }

    private void Start()
    {
        ResetTime();
    }

    private void Update()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime > maxTime - 1.5f) return;

            _alpha -= Time.deltaTime;
            var newColor = new Color(1, 1, 1, _alpha);
            messageText.color = newColor;            
        }
    }

    private void ResetTime()
    {
        _currentTime = maxTime;
        _alpha = 1;
    }

    private void SetTextMessage(string message)
    {        
        messageText.text = message.ToUpper();
        messageText.color = Color.white;
        ResetTime();
    }

    public void OnNotify(string message, params object[] args)
    {
        if (message != "MESSAGE") return;

        SetTextMessage((string)args[0]);
    }
}

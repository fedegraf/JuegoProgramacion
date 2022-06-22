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

    private void Awake()
    {
        player.GetComponent<Items.ItemLooter>().Suscribe(this);
        player.GetComponent<Items.ItemInteracter>().Suscribe(this);
        player.GetComponent<Skills.ExpandForce>().Suscribe(this);
    }

    public void OnNotify(string message, params object[] args)
    {
        if (message != "MESSAGE") return;

        SetTextMessage((string)args[0]);
    }

    private void SetTextMessage(string message)
    {        
        messageText.text = message.ToUpper();
        messageText.color = Color.white;
        StartCoroutine(DisplayMessage());
    }

    private IEnumerator DisplayMessage()
    {
        var currentTime = maxTime;
        var textColor = messageText.color;
        float alpha = 1;
        yield return new WaitForSeconds(1.5f);

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            alpha -= Time.deltaTime;
            textColor = new Color(textColor.r, textColor.g, textColor.b, alpha);
            messageText.color = textColor;
            yield return null;
        }

        yield return null;  
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCommand : ICommand
{
    private IDamagable _victim;
    private string _victimTag;

    public DieCommand(IDamagable victim, string tag = "")
    {
        _victim = victim;
        _victimTag = tag;
    }

    public void Do()
    {
        _victim.Die();
        if (_victimTag == "Player")
        {
            GameManager.Instance.SetState(GameManager.GameStates.Dead);
        }
    }
}

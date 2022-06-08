using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public GameObject Enemy { get; }
    public EnemyTypeSO Stats { get; }
    public bool CanMove { get; }
    public void EnableMovement(bool enable);
}

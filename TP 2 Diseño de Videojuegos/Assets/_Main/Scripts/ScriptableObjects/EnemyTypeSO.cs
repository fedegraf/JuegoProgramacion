using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Enemy", fileName = "Enemy Type", order = 1)]
public class EnemyTypeSO : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] private float speed;
    [SerializeField] private float attackRadius;

    public string EnemyName => enemyName;
    public float Speed => speed;

    public float AttackRadius => attackRadius;
}

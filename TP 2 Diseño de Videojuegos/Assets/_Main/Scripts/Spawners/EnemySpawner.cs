using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;
    private List<Transform> _spawnPoints = new List<Transform>();
    private bool _isUsed;
    private void Awake()
    {
        SetSpawnPoints();
    }

    private void SetSpawnPoints()
    {
        int childs = transform.childCount;

        if (childs == 0) return;


        for (int i = 0; i < childs; i++)
        {
            _spawnPoints.Add(transform.GetChild(i).transform);
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            var currentSpawn = _spawnPoints[i];
            GameObject newEnemy = Instantiate(enemyToSpawn, currentSpawn.position, currentSpawn.rotation, transform);
        }

        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            var currentChild = transform.GetChild(i);
            if (_spawnPoints.Contains(currentChild))
            {
                _spawnPoints.Remove(currentChild);
                Destroy(currentChild.gameObject);
            }
        }

        _isUsed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !_isUsed)
            SpawnEnemies();
    }
}

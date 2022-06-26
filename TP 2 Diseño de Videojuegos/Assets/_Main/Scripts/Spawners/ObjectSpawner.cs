using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToSpawn;

        private List<Transform> _spawnPoints = new List<Transform>();
        private void Awake()
        {
            SetSpawnPoints();
            SpawnPrefab();
        }

        private void SetSpawnPoints()
        {
            var chilCound = transform.childCount;
            if (chilCound == 0)
            {
                Debug.Log("No Childs");
                return;
            }
            for (int i = 0; i < chilCound; i++)
            {
                var spawnPoint = transform.GetChild(i);
                if (_spawnPoints.Contains(spawnPoint)) return;
                _spawnPoints.Add(spawnPoint);
            }
        }

        private void SpawnPrefab()
        {
            if (!prefabToSpawn) return;

            for (int i = 0; i < _spawnPoints.Count; i++)
            {
                var spawnPoint = _spawnPoints[i];
                Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation, transform);
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                var currentChild = transform.GetChild(i);
                if (_spawnPoints.Contains(currentChild))
                {
                    _spawnPoints.Remove(currentChild);
                    Destroy(currentChild.gameObject);
                }
            }
        }
    }
}


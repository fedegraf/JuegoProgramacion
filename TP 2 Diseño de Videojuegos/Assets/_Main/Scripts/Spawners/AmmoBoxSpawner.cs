using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class AmmoBoxSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToSpawn;
        [SerializeField] private Weapons.AmmoTypeSO ammoToUse;
        [SerializeField] private int ammoAmount = 10;

        private List<Transform> _spawnPoints = new List<Transform>();
        private void Awake()
        {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

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
                var ammoBox = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation, transform);
                ammoBox.GetComponent<AmmoBox>().SetValues(ammoToUse,ammoAmount);
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


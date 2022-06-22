using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyActivator : MonoBehaviour
    {
        private List<IEnemy> _enemiesToActivate = new List<IEnemy>();
        private bool _isUsed;

        private void Awake()
        {
            SetChildrenInList();
            GetComponent<BoxCollider>().isTrigger = true;
        }

        private void SetChildrenInList()
        {
            int childCount = transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                var child = transform.GetChild(i).GetComponent<IEnemy>();
                if (_enemiesToActivate.Contains(child)) return;

                _enemiesToActivate.Add(child);
            }

            ActivateChildrenInList(false);
        }

        private void ActivateChildrenInList(bool active)
        {
            for (int i = 0; i < _enemiesToActivate.Count; i++)
            {
                _enemiesToActivate[i].Enemy.SetActive(active);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isUsed) return;

            if (!other.CompareTag("Player")) return;

            ActivateChildrenInList(true);
            _isUsed = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    private List<GameObject> _objectsToActivate = new List<GameObject>();
    private bool _isUsed;

    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public void SetChildrenInList()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            if (_objectsToActivate.Contains(child)) return;

            _objectsToActivate.Add(child);
        }

        ActivateChildrenInList(false);
    }

    private void ActivateChildrenInList(bool active)
    {
        for (int i = 0; i < _objectsToActivate.Count; i++)
        {
            _objectsToActivate[i].SetActive(active);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class LaserSight : MonoBehaviour
    {
        [SerializeField] private string[] tagsToAvoid; 
        private LineRenderer lr;
        // Start is called before the first frame update
        void Start()
        {
            lr = GetComponent<LineRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider && CheckForTag(hit.collider.gameObject))
                {
                    float posmaxed = hit.distance * 100;
                    lr.SetPosition(1, new Vector3(0, 0, posmaxed));
                }
                else
                {
                    lr.SetPosition(1, new Vector3(0, 0, 4000));
                }
            }

        }

        private bool CheckForTag(GameObject gameObject)
        {
            for (int i = 0; i < tagsToAvoid.Length; i++)
            {
                if (gameObject.CompareTag(tagsToAvoid[i]))
                    return true;
            }

            return false;
        }

    }
}

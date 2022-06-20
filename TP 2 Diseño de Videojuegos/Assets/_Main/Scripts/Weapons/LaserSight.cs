using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class LaserSight : MonoBehaviour
    {
        [SerializeField] private LayerMask layersToAvoid;
        private LineRenderer lr;
        // Start is called before the first frame update
        void Start()
        {
            lr = GetComponent<LineRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hit))
            {
                if (hit.collider && hit.collider.gameObject.layer == layersToAvoid)
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
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
           if (hit.collider && !(hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("bullet")))
           {
               float posmaxed = hit.distance * 100;
               lr.SetPosition(1, new Vector3(0,0, posmaxed));
           }
            else
            {
                lr.SetPosition(1, new Vector3(0,0, 4000));
            }
        }

    }
}

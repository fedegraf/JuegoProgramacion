using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAimingScript : MonoBehaviour
{
    public bool isTargetInRange = false;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject Head;
    private float currentDistance;
    [SerializeField] private float maxDistanceTarget;
    private void Update()
    {
        if (target != null)
        {
            currentDistance = Vector3.Distance(target.transform.position, transform.position);
            if (currentDistance <= maxDistanceTarget)
            {
                Head.transform.LookAt(target.transform);
                isTargetInRange = true;
            }
            else
            {
                isTargetInRange = false;
            }  
        }
    }
}
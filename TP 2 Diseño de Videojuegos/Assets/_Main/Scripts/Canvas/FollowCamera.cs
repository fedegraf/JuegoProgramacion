using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Camera _cameraToFollow;
    // Update is called once per frame
    private void Start()
    {
        _cameraToFollow = Camera.main;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cameraToFollow.transform.position);
    }
}

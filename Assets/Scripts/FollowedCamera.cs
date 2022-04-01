using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowedCamera : MonoBehaviour
{
    public Transform target;
    public float smoothing = 4.5f;
    public Vector3 offset;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    private void Update()
    {
        if(target != null){
            Vector3 targetCamPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}

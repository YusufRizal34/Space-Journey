using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject target;
    public float smoothing = 4.5f;
    public float zOffset = 10f;

    private void Awake() {
        target = FindObjectOfType<CharacterControllers>().gameObject;
    }

    void Start()
    {
        transform.LookAt(target.transform);
    }

    void Update()
    {
        if(target != null){
            Vector3 targetCamPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z + zOffset);
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}

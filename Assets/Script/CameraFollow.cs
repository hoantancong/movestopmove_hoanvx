using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.125f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(target == null)
        {
            target = FindAnyObjectByType<Player>().TF;
        }
        else
        {
            Vector3 desiredPosition = target.position + offset;
            // move camera
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = desiredPosition;
            transform.LookAt(target);
        }
    }
}

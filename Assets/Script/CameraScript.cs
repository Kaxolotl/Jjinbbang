using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float dist = 10.0f;
    public float height = 5.0f;

    private Transform _transform;

    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        _transform.position = target.position - (Vector3.forward * dist);
        _transform.LookAt(target);
    }
}
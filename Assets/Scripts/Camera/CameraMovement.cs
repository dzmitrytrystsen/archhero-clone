using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = new Vector3(0f, 8f, 0f);

    private Transform _target;

    private void Start()
    {
        _target = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        float smoothSpeed = 0.1f;

        Vector3 desiredPosition = new Vector3(0, _target.position.y + _offset.y, _target.position.z + _offset.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}

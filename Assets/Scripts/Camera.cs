using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float distance;

    private float _maxY = 70;
    private float _minY = -10;
    private float _currentX;
    private float _currentY;
    
    private Transform _target;

    private void Awake()
    {
        _target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _currentX += Input.GetAxis("Mouse X");
        _currentY -= Input.GetAxis("Mouse Y");

        _currentY = Mathf.Clamp(_currentY, _minY, _maxY);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(_currentY,_currentX,0);
        transform.position = _target.position + rotation * dir;
        transform.LookAt(_target.position);
    }
}

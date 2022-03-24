using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _distance = 5;
    [SerializeField] private float _sensitivity;

    private Vector3 _previousPosition;

    private void Start()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, _target.position.z - _distance);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _previousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if(Input.GetMouseButton(0))
        {
            Vector3 direction = _previousPosition - Camera.main.ScreenToViewportPoint(Input.mousePosition);

            transform.position = _target.position;

            transform.Rotate(Vector3.right, direction.y * 180 * _sensitivity);
            transform.Rotate(Vector3.up, -direction.x * 180 * _sensitivity, Space.World);
            transform.Translate(new Vector3(_target.position.x, _target.position.y, _target.position.z - _distance));

            _previousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        }
    }
}

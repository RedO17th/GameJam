using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private BasePlayer _player;
    [SerializeField] private Vector3 _distance;
    [SerializeField] private float _speed = 10f;

    public Quaternion Rotation => transform.rotation;


    private void LateUpdate()
    {
        Movement();

        Rotate();
    }

    private void Movement()
    {
        //transform.position = _player.transform.position + _distance;
        transform.position = _player.Position + _distance;
    }

    private void Rotate()
    {
        //if (Input.GetKey(KeyCode.E))
        //{
        //    transform.Rotate(0, -_speed * Time.deltaTime, 0);
        //}
        //else if (Input.GetKey(KeyCode.Q))
        //{
        //    transform.Rotate(0, _speed * Time.deltaTime, 0);
        //}

        if (Input.GetKey(KeyCode.E)) RotateByDirection(-_speed);

        if (Input.GetKey(KeyCode.Q)) RotateByDirection(_speed);
    }

    private void RotateByDirection(float direction)
    {
        transform.Rotate(0f, direction * Time.deltaTime, 0f);
    }
}

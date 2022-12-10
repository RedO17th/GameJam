using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    [Range(0.5f, 5f)]
    [SerializeField] private float _speed = 1f;

    [Range(0.1f, 100f)]
    [SerializeField] private float _speedRotation = 1f;

    [SerializeField] private LayerMask _groundMask;

    private CharacterController _charController = null;

    //TODO: Remove object
    private GameObject _obj = null;

    private void Awake()
    {
        _charController = GetComponent<CharacterController>();

        //TODO: Remove object
        _obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _obj.transform.position = Vector3.zero;
    }

    void Update()
    {
        Rotation();
        Movement();
    }

    private void Rotation()
    {
        var mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundMask))
        {
            _obj.transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            var direction = (_obj.transform.position - transform.position).normalized;
            var rotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speedRotation * Time.deltaTime);
        }           
    }

    private void Movement()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        _charController.Move(transform.rotation * move * Time.deltaTime * _speed);
    }
}

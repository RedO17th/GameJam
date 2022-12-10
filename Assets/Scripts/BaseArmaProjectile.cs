using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseArmaProjectile : MonoBehaviour
{
    private Rigidbody _rigidBody = null;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void Launch()
    {
        _rigidBody.AddForce(transform.forward, ForceMode.Impulse);
    }
}

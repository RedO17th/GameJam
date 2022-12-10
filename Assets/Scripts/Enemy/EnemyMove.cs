using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float _speed = 5;

    private CharacterController _characterController;
    private Transform _target;

    private float _defaultSpeed;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _defaultSpeed = _speed;
    }

    private void Update()
    {
        if (_target == null)
            return;

        Vector3 targetVector = (_target.position - transform.position).normalized;
        targetVector.y = 0;

        if (targetVector.Equals(Vector3.zero))
            return;

        _characterController.SimpleMove(_speed * targetVector);
        _characterController.transform.rotation = Quaternion.LookRotation(targetVector);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void RaiseSpeed(float percent)
    {
        _speed += _speed * percent;
    }

    public void SetSpeedToDefault()
    {
        _speed = _defaultSpeed;
    }
}

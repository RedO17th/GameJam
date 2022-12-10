using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float _speed = 5;

    private CharacterController _characterController;
    private Transform _target;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_target == null)
            return;

        Vector3 targetVector = (_target.position - transform.position).normalized;
        targetVector.y = 0;

        _characterController.Move(_speed * Time.deltaTime * targetVector);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}

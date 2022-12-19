using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] float _speed = 5;

    
    private BasePlayer _target;

    private float _defaultSpeed;

    private void Awake()
    {
        _defaultSpeed = _speed;
    }

    private void Update()
    {
        if (GameParameters.GameRunning == false) return;
        
        if (_target == null)
            return;

        Vector3 targetVector = (_target.Position - transform.position).normalized;
                targetVector.y = 0;

        if (targetVector.Equals(Vector3.zero))
            return;

        //..
        Rotate(targetVector);
        Move(targetVector);
        //
    }

    private void Move(Vector3 targetVector)
    {
        _characterController.SimpleMove(_speed * targetVector);
    }

    private void Rotate(Vector3 targetVector)
    {
        _characterController.transform.rotation = Quaternion.LookRotation(targetVector);
    }

    public void SetTarget(BasePlayer target) => _target = target;

    //Increase?
    public void RaiseSpeed(float percent)
    {
        _speed += _speed * percent;
    }

    public void SetSpeedToDefault() => _speed = _defaultSpeed;

}

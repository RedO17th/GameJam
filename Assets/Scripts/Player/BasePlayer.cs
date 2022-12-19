using System;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    [SerializeField] private CameraControl _cameraController;
    [SerializeField] private AbilitySystem _abilitySystem = null;

    [SerializeField] private Animator _aController = null;

    [Space]
    [Range(0.5f, 5f)]
    [SerializeField] private float _speed = 1f;

    [Range(0.1f, 100f)]
    [SerializeField] private float _speedRotation = 1f;

    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private int _startGold = 100;

    public event Action OnUseAbility;

    public int StartGold => _startGold;
    public Vector3 Position => transform.position;

    private CharacterController _charController = null;

    private float _horInput = 0f;
    private float _vertInput = 0f;


    private void Awake()
    {
        _charController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _abilitySystem.Initialize(this);
        EventManager.OnEnemyKilled.AddListener(EnemyKilled);
    }

    private void OnDisable()
    {
        _abilitySystem.Deinitialize();
    }


    void Update()
    {
        if (GameParameters.GameRunning == false) return;

        UseAbility();

        Rotation();
        Movement();

        //Обернуть методом
        _aController.SetFloat("Vertical", _vertInput);
        _aController.SetFloat("Horizontal", _horInput);
    }

    #region Movement

    private void Rotation()
    {
        var mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundMask))
        {
            var hitPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            var direction = (hitPosition - transform.position).normalized;
            var rotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speedRotation * Time.deltaTime);
        }           
    }
    private void Movement()
    {
        _horInput = Input.GetAxis("Horizontal");
        _vertInput = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(_horInput, 0, _vertInput);

        Vector3 gravity = (transform.position.y > 0) ? Vector3.down : Vector3.zero;

        _charController.Move(_cameraController.Rotation * (move + gravity) * Time.deltaTime * _speed);
    }

    #endregion

    private void UseAbility()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(_abilitySystem.AbilityPrice);
            OnUseAbility?.Invoke();
        }
    }   
    
    public void TakeDamage(int damage) => Score.GoldDecrease(damage);

    private void EnemyKilled(Enemy enemy) => AddGold(enemy.Price);

    public static void AddGold(int value) => Score.GoldIncrease(value);
}
 
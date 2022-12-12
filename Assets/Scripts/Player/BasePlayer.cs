using System;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    [SerializeField] private GameObject _cameraController;
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

    public float horInput = 0f;
    public float vertInput = 0f;


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
        if (!GameParameters.GameRunning) return;

        UseAbility();

        Rotation();
        Movement();

        _aController.SetFloat("Vertical", vertInput);
        _aController.SetFloat("Horizontal", horInput);
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
        horInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horInput, 0, vertInput);

        Vector3 gravity = Vector3.zero;
        if(transform.position.y > 0)
        {
            gravity = Vector3.down;
        }
        else
        {
            gravity = Vector3.zero;
        }
        
        _charController.Move(_cameraController.transform.rotation * (move + gravity) * Time.deltaTime * _speed);
    }

    #endregion

    private void UseAbility()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //_aController.SetBool("Shoot", true);

            TakeDamage(_abilitySystem.AbilityPrice);
            OnUseAbility?.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            //_aController.SetBool("Shoot", false);
        }
    }   
    
    public void TakeDamage(int damage)
    {
        //_wallet -= damage;
        //EventManager.SendGoldChanged(-damage);
        //if(_wallet <= 0)
        //{
        //    _wallet = 0;
        //    EventManager.SendGameOver();
        //}

        Score.GoldDecrease(damage);
    }

    private void EnemyKilled(Enemy enemy)
    {
        AddGold(enemy.Price);
    }

    public static void AddGold(int value)
    {
        //_wallet += value;
        //EventManager.SendGoldChanged(value);

        Score.GoldIncrease(value);
    }
}
 
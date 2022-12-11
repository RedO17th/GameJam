using System;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    [SerializeField]  private AbilitySystem _abilitySystem = null;

    [Space]
    [Range(0.5f, 5f)]
    [SerializeField] private float _speed = 1f;

    [Range(0.1f, 100f)]
    [SerializeField] private float _speedRotation = 1f;

    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private int _wallet = 100;

    public event Action OnUseAbility;

    private CharacterController _charController = null;


    private void Awake()
    {
        _charController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _abilitySystem.Initialize(this);
    }

    private void OnDisable()
    {
        _abilitySystem.Deinitialize();
    }


    void Update()
    {
        UseAbility();

        Rotation();
        Movement();
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
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        _charController.Move(move * Time.deltaTime * _speed);
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
    
    public void TakeDamage(int damage)
    {
        _wallet -= damage;
        EventManager.SendGoldChanged(-damage);
        if(_wallet <= 0)
        {
            _wallet = 0;
            EventManager.SendGameOver();
        }
    }
}
 
using System.Collections;
using Photon.Pun;
using UnityEngine;

public class PlayerInput : MonoBehaviourPun
{
    public LayerMask groundMask;
    
    private float _horizontalMovement;
    private float _verticalMovement;
    private float _currentAttackRate;
    private Vector3 _moveDirection;
    private Rigidbody _rb;
    private CapsuleCollider _col;
    private PlayerState _state;
    private Animator _animator;

    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
    
    private void Awake()
    {
        if(!photonView.IsMine) return;
        
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _state = GetComponent<PlayerState>();
        _animator = GetComponentInChildren<Animator>();
        
        _currentAttackRate = 0;
    }

    private void FixedUpdate()
    {
        if(!photonView.IsMine) return; 
        
        ProcessInput();

        if (CheckGround() && Input.GetKeyDown(KeyCode.Space))
            Jump();
        
        if (_currentAttackRate <= 0 && CheckGround() && Input.GetKeyDown(KeyCode.LeftControl))
            StartCoroutine(Attack());
        else
            _currentAttackRate -= Time.deltaTime;
    }
    
    private IEnumerator Attack()
    {
        _animator.SetBool(IsAttacking, true);
        _state.hitBox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        
        _animator.SetBool(IsAttacking, false);
        _state.hitBox.gameObject.SetActive(false);
        
        _currentAttackRate = _state.attackRate;
    }

    private void ProcessInput()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _verticalMovement = Input.GetAxisRaw("Vertical");

        Vector3 movementDirection = new Vector3(-_horizontalMovement, 0, -_verticalMovement);
        movementDirection.Normalize();
        
        transform.Translate(movementDirection * _state.speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            _animator.SetBool(IsRunning, true);
            
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, _state.rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Player is not running
            _animator.SetBool(IsRunning, false);    
        }
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _state.jumpForce, ForceMode.Impulse);
    }

    private bool CheckGround()
    {
        Bounds bounds = _col.bounds;
        bool isGrounded = Physics.CheckCapsule(bounds.center, new Vector3(bounds.center.x, bounds.min.y, bounds.center.z), _col.radius * .9f, groundMask);
        
        _animator.SetBool(IsJumping, !isGrounded);
        
        return isGrounded;
    }
}

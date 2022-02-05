using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public LayerMask groundMask;

    private float _currentFireRate;
    private float _horizontalMovement;
    private float _verticalMovement;
    private Vector3 _moveDirection;
    private Rigidbody _rb;
    private CapsuleCollider _col;
    private Camera _cam;
    private PlayerState _state;
    private Animator _animator;
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsRunnig = Animator.StringToHash("IsRunning");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _cam = Camera.main;
        _state = GetComponent<PlayerState>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        ProcessInput();

        if (CheckGround() && Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void ProcessInput()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _verticalMovement = Input.GetAxisRaw("Vertical");

        Vector3 movementDirection = new Vector3(_horizontalMovement, 0, _verticalMovement);
        movementDirection.Normalize();
        
        transform.Translate(movementDirection * _state.speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            _animator.SetBool(IsRunnig, true);
            
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, _state.rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Player is not running
            _animator.SetBool(IsRunnig, false);    
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

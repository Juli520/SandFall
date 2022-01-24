using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5;
    public float jumpForce = 7;
    public LayerMask mask;

    private float _currentFireRate;
    private float _horizontalMovement;
    private float _verticalMovement;
    private Vector3 _moveDirection;
    private Rigidbody _rb;
    private CapsuleCollider _col;
    private Camera _cam;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        ProcessInput();
        MovePlayer();

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            Jump();
        
        transform.forward = new Vector3(_cam.transform.forward.x, 0, _cam.transform.forward.z);
    }

    private void ProcessInput()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _verticalMovement = Input.GetAxisRaw("Vertical");

        _moveDirection = transform.forward * _verticalMovement + transform.right * _horizontalMovement;
    }

    private void MovePlayer()
    {
        _rb.velocity = new Vector3(_moveDirection.normalized.x * speed, _rb.velocity.y, _moveDirection.normalized.z * speed);
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        Bounds bounds = _col.bounds;
        return Physics.CheckCapsule(bounds.center, new Vector3(bounds.center.x, bounds.min.y, bounds.center.z), _col.radius * .9f, mask);
    }
}

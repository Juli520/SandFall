using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 7;
    
    public LayerMask mask;

    private float horizontalMovement;
    private float verticalMovement;
    private Vector3 moveDirection;
    
    private Rigidbody _rb;
    private CapsuleCollider _col;
    private Camera _cam;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Start()
    {
        transform.forward = new Vector3(_cam.transform.forward.x, 0, _cam.transform.forward.z);
    }

    void Update()
    {
        ProccesInput();
        MovePlayer();

        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void ProccesInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }

    private void MovePlayer()
    {
        _rb.velocity = new Vector3(moveDirection.normalized.x * speed, _rb.velocity.y, moveDirection.normalized.z * speed);
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private bool isGrounded()
    {
        return Physics.CheckCapsule(_col.bounds.center, new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z), _col.radius * .9f, mask);
    }
}

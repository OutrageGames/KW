using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FishNet.Object;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpSpeed;
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float JumpSpeed { get => _jumpSpeed; set => _jumpSpeed = value; }
    private float _curDir;
    [SerializeField] private float _groundCheckY, _groundCheckSizeX, _groundCheckSizeY;
    [SerializeField] private LayerMask _groundMask;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private bool _isGrounded;

    public override void OnStartClient()
    {
        base.OnStartClient();
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.isKinematic = !IsOwner;
        if(!base.IsOwner)
        {
            //gameObject.tag = "Enemy";
            //gameObject.layer = 8;
        }
        else
        {
            //gameObject.tag = "Player";
            //gameObject.layer = 6;
        }
    }
   
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();    
    }
    private void Move(float dir)
    {
        _rigidbody.velocity = new Vector2(_moveSpeed * dir, _rigidbody.velocity.y);
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - _groundCheckY),
                new Vector2(_groundCheckSizeX, _groundCheckSizeY), 0f, _groundMask);

        if(!IsOwner && _isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
        }

        Move(_curDir);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - _groundCheckY),
            new Vector2(_groundCheckSizeX, _groundCheckSizeY));
    }
    

    private void Jump()
    {
        if(_isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpSpeed);
        }
    }

    public void MoveCallback(InputAction.CallbackContext context)
    {
        if(!base.IsOwner)
            return;


        _curDir = context.ReadValue<float>();        
    }

    public void JumpCallback(InputAction.CallbackContext context)
    {
        if(!base.IsOwner)
            return;

        if (context.performed)
        {
            Jump();
        }
    }
    
}

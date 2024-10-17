using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
    private BoxCollider2D _collider;
    private Rigidbody2D _rb;
    [SerializeField]
    private float _movementSpeed = 5f;
    [SerializeField]
    private float _jumpVelocity = 5f;
    [SerializeField]
    PlayerAnimation _playerAnimation;

    [SerializeField] LayerMask _groundedMask;

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 
            _groundedMask);

        return (hit.collider != null);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector2.down * 0.6f);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Assign ReigidBody2D
        _rb = GetComponent<Rigidbody2D>();
        Assert.IsNotNull( _rb , "No Rigidbody2D found on " + this.gameObject.name);
        _collider = GetComponent<BoxCollider2D>();
        Assert.IsNotNull(_collider, "No BoxCollider2D found on " + this.gameObject.name);
        if (_playerAnimation == null) {
            _playerAnimation = GetComponent<PlayerAnimation>();
            Assert.IsNotNull(_playerAnimation, "No Player Animation set for " + this.gameObject.name);
        }
    }

    private void Update()
    {
        // Detect jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpVelocity);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(horizontal * _movementSpeed, _rb.velocity.y);
        _playerAnimation.SetMoveSpeed(Mathf.Abs(horizontal));
    }
}

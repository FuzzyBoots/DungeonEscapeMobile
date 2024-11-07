using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
    private const float GROUNDED_RAY_LENGTH = 0.8f;
    private BoxCollider2D _collider;
    private Rigidbody2D _rb;
    [SerializeField]
    private float _movementSpeed = 5f;
    [SerializeField]
    private float _jumpVelocity = 5f;
    [SerializeField]
    PlayerAnimation _playerAnimation;
    [SerializeField]
    SpriteRenderer _spriteRenderer;

    [SerializeField] bool _isJumping = false;

    [SerializeField] LayerMask _groundedMask;

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, GROUNDED_RAY_LENGTH, 
            _groundedMask);

        bool isGrounded = (hit.collider != null);
        
        return isGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector2.down * GROUNDED_RAY_LENGTH);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetComponentIfNull<Rigidbody2D>(ref _rb, "Rigidbody 2D");
        SetComponentIfNull<BoxCollider2D>(ref _collider, "Box Collider 2D");
        SetComponentIfNull<PlayerAnimation>(ref _playerAnimation, "Player Animation script");
        SetComponentIfNull<SpriteRenderer>(ref _spriteRenderer, "Sprite Renderer");
    }

    private void SetComponentIfNull<T>(ref T field, string fieldName)
    {
        if (field == null)
        {
            field = GetComponent<T>();
            Assert.IsNotNull(_playerAnimation, $"No {fieldName} set for " + this.gameObject.name);
        }
    }

    private WaitForSeconds waitForJumpDelay = new WaitForSeconds(0.1f);

    private IEnumerator SetJumping()
    {
        // We want a brief delay so that we don't register as jumping until we're actually off the ground
        yield return waitForJumpDelay;
        _isJumping = true;
    }

    private void Update()
    {
        bool isGrounded = IsGrounded();
        // Detect jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                _playerAnimation.SetJumping(true);
                StartCoroutine(SetJumping());
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpVelocity);
            }
        }

        if (_isJumping && isGrounded)
        {
            _isJumping = false;
            _playerAnimation.SetJumping(false);
        }

        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            _playerAnimation.SetAttack();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0.01f)
        {
            _spriteRenderer.flipX = false;
        } else if (horizontal < -0.01f)
        {
            _spriteRenderer.flipX = true;
        }

        _rb.velocity = new Vector2(horizontal * _movementSpeed, _rb.velocity.y);
        _playerAnimation.SetMoveSpeed(Mathf.Abs(horizontal));
    }
}

using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float GROUNDED_RAY_LENGTH = 0.8f;
    [SerializeField]
    private BoxCollider2D _collider;
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private float _movementSpeed = 5f;
    [SerializeField]
    private float _jumpVelocity = 5f;
    [SerializeField]
    PlayerAnimation _playerAnimation;
    [SerializeField]
    SpriteRenderer _playerSpriteRenderer;
    [SerializeField]
    SpriteRenderer _swordFlashSpriteRenderer;

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
        SetComponentIfNull<SpriteRenderer>(ref _playerSpriteRenderer, "Sprite Renderer");
    }

    private void SetComponentIfNull<T>(ref T field, string fieldName)
    {
        if (field == null)
        {
            field = GetComponent<T>();
            if (field == null) { Debug.LogError($"No {fieldName} set for {gameObject.name}"); }
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
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        } else if (horizontal < -0.01f)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        _rb.velocity = new Vector2(horizontal * _movementSpeed, _rb.velocity.y);
        _playerAnimation.SetMoveSpeed(Mathf.Abs(horizontal));
    }
}

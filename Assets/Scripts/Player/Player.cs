using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
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

    [SerializeField] int _initialHealth = 5;

    [SerializeField] private bool _isDead = false;

    [SerializeField] private int _diamonds;

    [SerializeField] private int _health;

    public int Health { get { return _health; } set { _health = value; } }
    public int Diamonds { get { return _diamonds; }}

    public bool FlamingSwordActive { get; set; }
    public bool FlyingBootsActive { get; set; }
    public bool CastleKeyActive { get; set; }

    DungeonEscapeControls _controls;

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

        Health = _initialHealth;
        HUD_UI_Manager.Instance.SetLife(Health);
        HUD_UI_Manager.Instance.SetGemCount(Diamonds);

        _controls = new DungeonEscapeControls();
        _controls.DungeonEscape.Enable();

        _controls.DungeonEscape.Attack.performed += Attack_performed;
        _controls.DungeonEscape.Jump.performed += Jump_performed;
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (IsGrounded())
        {
            _playerAnimation.SetJumping(true);
            StartCoroutine(SetJumping());
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpVelocity);
        }
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (IsGrounded())
        {
            _playerAnimation.SetAttack();
        }
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
        if (_isDead) return;

        if (_playerAnimation.BeingHit()) return;

        bool isGrounded = IsGrounded();
        
        if (_isJumping && isGrounded)
        {
            _isJumping = false;
            _playerAnimation.SetJumping(false);
        }        
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = _controls.DungeonEscape.Move.ReadValue<Vector2>().x;
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

    public bool IsDead()
    {
        return _isDead;
    }

    public void Damage(int damage)
    {
        if (_isDead)
        {
            return;
        }
        _playerAnimation.SetHit();
        Health -= damage;
        HUD_UI_Manager.Instance.SetLife(Health);
        Debug.Log($"Player Damaged for {damage}. Health is {Health}");
        if (Health <= 0)
        {
            _playerAnimation.SetDeath();
            _isDead = true;
        }
    }

    public void AddDiamonds(int diamondValue)
    {
        _diamonds += diamondValue;

        HUD_UI_Manager.Instance.SetGemCount(_diamonds);
    }

    public void RemoveDiamonds(int diamondValue)
    {
        _diamonds -= diamondValue;

        HUD_UI_Manager.Instance.SetGemCount(_diamonds);
    }
}

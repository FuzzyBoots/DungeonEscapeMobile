using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _gems;

    [SerializeField] Animator _animator;

    [SerializeField] protected Transform[] _travelPoints;
    
    [SerializeField] protected int _travelPointIndex = 0;
    [SerializeField] protected bool _travelPointForward;

    [SerializeField] float _wayPointThreshold = 0.05f;
    [SerializeField] protected float _sightDistance = 2f;

    [SerializeField] protected bool _isDead = false;

    protected Player _player;
    [SerializeField] Diamond _diamond;

    [SerializeField] AudioClip _attackSound;
    [SerializeField] AudioClip _deathSound;
    [SerializeField] AudioClip _footstepSound;
    [SerializeField] AudioClip _hitSound;

    AudioSource _audioSource;

    public bool CombatMode { 
        get { return _animator ? _animator.GetBool("Combat Mode") : false; } 
        set { _animator?.SetBool("Combat Mode", value);  } 
    }

    private void Start()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
        if (_animator == null)
        {
            Debug.LogError("Animator could not be determined.", this);
        }

        _audioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(_audioSource, "No audio source found on enemy " + gameObject.name);

        _player = GameObject.Find("Player")?.GetComponent<Player>();
    }

    public void Attack()
    {
        Debug.Log($"{gameObject.name} is attacking!");
    }

    private void Update()
    {
        HandleUpdate();
    }

    protected virtual void HandleUpdate()
    {
        if (!_isDead)
        {
            HandleMovement();
        }
    }

    protected void HandleMovement()
    {
        AnimatorStateInfo curState = _animator.GetCurrentAnimatorStateInfo(0);

        if (!curState.IsName("Walk"))
        {
            return;
        }

        Vector3 nextPos = _travelPoints[_travelPointIndex].position;
        if (nextPos.x < transform.position.x)
        {
            FlipSprite(true);
        }
        else if (nextPos.x > transform.position.x)
        {
            FlipSprite(false);
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextPos) < _wayPointThreshold)
        {
            _animator.SetTrigger("Idle");

            if (_travelPointForward)
            {
                _travelPointIndex = (_travelPointIndex + 1) % _travelPoints.Length;
            }
            else
            {
                _travelPointIndex = (_travelPointIndex - 1 + _travelPoints.Length) % _travelPoints.Length;
            }
        }
    }

    protected void FlipSprite(bool flip)
    {
        Vector3 curScale = transform.localScale;
        transform.localScale = new Vector3((flip ? -1 : 1) * Mathf.Abs(curScale.x), curScale.y, curScale.z);
    }

    public void PlayHit()
    {
        _animator.SetTrigger("Hit");
        _audioSource.PlayOneShot(_hitSound);
    }

    public void PlayDeath()
    {
        _animator.SetTrigger("Death");
        _audioSource.PlayOneShot(_deathSound);
        Diamond diamond = Instantiate(_diamond, transform.position, Quaternion.identity);
        diamond.SetValue(_gems);
        _isDead = true;
    }
}

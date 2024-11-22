using UnityEngine;

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
    }

    public void Attack()
    {
        Debug.Log($"{gameObject.name} is attacking!");
    }

    private void Update()
    {
        HandleUpdate();
    }

    protected void HandleUpdate()
    {
        HandleMovement();
    }

    protected void HandleMovement()
    {
        AnimatorStateInfo curState = _animator.GetCurrentAnimatorStateInfo(0);

        if (curState.IsName("Idle"))
        {
            return;
        }

        Vector3 nextPos = _travelPoints[_travelPointIndex].position;
        if (nextPos.x < transform.position.x)
        {
            Vector3 curScale = transform.localScale;
            transform.localScale = new Vector3(-Mathf.Abs(curScale.x), curScale.y, curScale.z);
        }
        else if (nextPos.x > transform.position.x)
        {
            Vector3 curScale = transform.localScale;
            transform.localScale = new Vector3(Mathf.Abs(curScale.x), curScale.y, curScale.z);
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

    public void PlayHit()
    {
        _animator.SetTrigger("Hit");
    }
}

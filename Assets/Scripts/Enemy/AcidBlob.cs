using UnityEngine;

public class AcidBlob : MonoBehaviour
{
    [SerializeField] bool _movingRight = true;
    [SerializeField] float _speed = 1f;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void SetMoveRight(bool moveRight)
    {
        _movingRight = moveRight;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((_movingRight ? 1 : -1) * _speed * Time.deltaTime * Vector3.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.Damage(1);
        }
        Destroy(gameObject);
    }
}

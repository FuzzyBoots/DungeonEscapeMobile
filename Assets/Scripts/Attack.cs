using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] int _damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(_damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] int _damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Hit {collision.gameObject.name}");
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(_damage);
        }
    }
}

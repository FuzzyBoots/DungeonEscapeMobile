using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    WaitForSeconds _mercyInvincibilityTime = new WaitForSeconds(0.5f);
    private bool _isDamageable = true;

    public float health { get; set; }

    public IEnumerator DamageCooldown()
    {
        yield return _mercyInvincibilityTime;
        _isDamageable = true;
    }
    public void Damage(float damage)
    {
        Debug.Log("Skeleton taking damage");
        if (_isDamageable)
        {
            health -= damage;
            PlayHit();

            StartCoroutine(DamageCooldown());
        }
    }

}

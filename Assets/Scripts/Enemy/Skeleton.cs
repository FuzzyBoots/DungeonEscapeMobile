using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    WaitForSeconds _mercyInvincibilityTime = new WaitForSeconds(0.5f);
    private bool _isDamageable = true;

    public int Health { get { return _health; } set { _health = value; } }
    
    public IEnumerator DamageCooldown()
    {
        yield return _mercyInvincibilityTime;
        _isDamageable = true;
    }

    public void Damage(int damage)
    {
        if (_isDamageable && Health > 0)
        {
            Debug.Log($"Skeleton taking damage {damage} - curHealth: {Health}");
            PlayHit();
            Health -= damage;
            
            if (Health <= 0) {
                PlayDeath();
            }

            StartCoroutine(DamageCooldown());
        }
    }

}

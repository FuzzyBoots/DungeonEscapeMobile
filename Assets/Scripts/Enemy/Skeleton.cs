using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    WaitForSeconds _mercyInvincibilityTime = new WaitForSeconds(0.5f);
    bool _isDamageable = true;
    
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
            PlayHit();
            SetCombatMode(true);
            Health -= damage;
            
            if (Health <= 0) {
                PlayDeath();
            }

            StartCoroutine(DamageCooldown());
        }
    }

    protected override void HandleUpdate()
    {
        base.HandleUpdate();

        if (Vector3.Distance(transform.localPosition, _player.gameObject.transform.localPosition) > _sightDistance)
        {
            SetCombatMode(false);
        }
    }
}

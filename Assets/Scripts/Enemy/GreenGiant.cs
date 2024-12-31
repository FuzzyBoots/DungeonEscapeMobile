using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGiant : Enemy, IDamageable
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
            _isDamageable = false;
            CombatMode = true;
            Health -= damage;
            Debug.Log($"Health is {Health}");

            if (Health <= 0)
            {
                PlayDeath();
            }

            StartCoroutine(DamageCooldown());
        }
    }

    protected override void HandleUpdate()
    {
        base.HandleUpdate();

        // Find direction to player
        Vector3 direction = _player.transform.position - transform.position;

        if (Vector3.Distance(transform.localPosition, _player.gameObject.transform.localPosition) > _sightDistance)
        {
            CombatMode = false;
        }

        if (CombatMode)
        {
            if (direction.x > 0)
            {
                FlipSprite(false);
            }
            else if (direction.x < 0)
            {
                FlipSprite(true);
            }
        }
    }
}

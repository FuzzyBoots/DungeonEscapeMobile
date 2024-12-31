using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    [SerializeField] AcidBlob _orb;
    public int Health { get; set; }

    protected override void HandleUpdate()
    {
        base.HandleUpdate();

        // Find direction to player
        Vector3 direction = _player.transform.position - transform.position;

        CombatMode = Vector3.Distance(transform.localPosition, _player.gameObject.transform.localPosition) < _sightDistance;

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

    public void Damage(int damage)
    {
        PlayDeath();
    }

    public void FireOrb()
    {
        AcidBlob blob = Instantiate(_orb, transform.position + Vector3.up * 1.576f, Quaternion.identity);
        if (transform.localScale.x < 0)
        {
            blob.SetMoveRight(false);
        }
        else
        {
            blob.SetMoveRight(true);
        }
    }
}

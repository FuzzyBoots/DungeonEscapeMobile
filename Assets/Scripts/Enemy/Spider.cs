using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public float health { get; set; }

    public void Damage(float damage)
    {
        PlayHit();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGiant : Enemy, IDamageable
{
    public int Health { get; set; }

    public void Damage(int damage)
    {
        PlayHit();
    }
}

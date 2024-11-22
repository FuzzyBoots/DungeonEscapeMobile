using System;

public interface IDamageable
{
    float health { get; set; }
    
    void Damage(float damage);
}

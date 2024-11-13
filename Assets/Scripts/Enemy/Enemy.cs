using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _gems;

    [SerializeField] protected Transform[] _travelPoints;
    
    public void Attack()
    {
        Debug.Log($"{gameObject.name} is attacking!");
    }
}

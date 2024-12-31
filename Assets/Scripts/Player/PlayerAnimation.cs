using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator _playerAnimator;
    [SerializeField] Animator _slashAnimator;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(_playerAnimator, "Animator not assigned for player" );
        Assert.IsNotNull(_slashAnimator, "Animator not assigned for slash");
    }

    public void SetMoveSpeed(float speed) {
        _playerAnimator.SetFloat("Movement Speed", speed);
    }

    public void SetJumping(bool jumping)
    {
        _playerAnimator.SetBool("Jumping", jumping);
    }

    public void SetAttack()
    {
        if (!_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player Attack"))
        {
            _playerAnimator.SetTrigger("Attack");
            _slashAnimator.SetTrigger("Slash");
        }
    }

    public void SetDeath()
    {
        _playerAnimator.SetTrigger("Death");
    }

    public bool BeingHit()
    {
        return _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player Hit");
    }

    internal void SetHit()
    {
        _playerAnimator.SetTrigger("Hit");
    }
}

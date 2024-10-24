using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(_animator, "Animator not assigned for player" );
    }

    public void SetMoveSpeed(float speed) {
        _animator.SetFloat("Movement Speed", speed);
    }

    public void SetJumping(bool jumping)
    {
        _animator.SetBool("Jumping", jumping);
    }
}

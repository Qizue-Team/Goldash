using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
public class PlayerAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private PlayerJump playerJump;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("IsFastFalling", playerJump.IsFastFalling);
    }
}

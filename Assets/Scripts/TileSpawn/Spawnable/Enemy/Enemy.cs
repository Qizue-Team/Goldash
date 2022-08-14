using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : Spawnable
{
    [Header("Enemy Settings")]
    [SerializeField]
    protected float fallingSpeed = 2.0f;

    protected Animator _animator;
    protected bool _isDying = false;

    public void Die()
    {
        _animator.SetTrigger("Die");
        _isDying = true;
    }

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        if (_isDying)
        {
            transform.position+=Vector3.down * Time.deltaTime * fallingSpeed;
        }
    }
}

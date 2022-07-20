using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public bool IsOpen { get; private set; }

    private Animator _animator;
    public void Enter()
    {
        if (IsOpen)
            return;
        _animator.SetTrigger("Enter");
        IsOpen = true;
    }

    public void Exit()
    {
        if (!IsOpen)
            return;
        _animator.SetTrigger("Exit");
        IsOpen = false;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        IsOpen = false;
    }
}

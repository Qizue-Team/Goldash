using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LoadingScreen : MonoBehaviour
{
    public bool IsOpen { get; private set; }

    private Animator _animator;

    public void Open()
    {
        if (IsOpen)
            return;
        IsOpen = true;
        _animator.SetTrigger("Open");
    }

    public void Close()
    {
        if (!IsOpen)
            return;
        IsOpen = false;
        _animator.SetTrigger("Close");
    }

    private void Awake()
    {
        IsOpen = true;
        _animator = GetComponent<Animator>();
    }
}

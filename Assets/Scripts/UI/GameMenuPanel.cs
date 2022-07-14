using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class GameMenuPanel : MonoBehaviour
{
    public bool IsOpen { get; private set; }

    [SerializeField]
    private Image blackFadeScreen;

    private Animator _animator;

    public void Open()
    {
        if (IsOpen)
            return;

        blackFadeScreen.enabled = true;

        IsOpen = true;
        _animator.SetTrigger("Open");
    }

    public void Close()
    {
        if (!IsOpen)
            return;

        IsOpen = false;
        _animator.SetTrigger("Close");
        StartCoroutine(COWaitForAction(1.0f, () => blackFadeScreen.enabled = false));
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private IEnumerator COWaitForAction(float delay, Action ActionToPerform)
    {
        yield return new WaitForSeconds(delay);
        ActionToPerform?.Invoke();
    }
}

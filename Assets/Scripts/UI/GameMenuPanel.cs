using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class GameMenuPanel : MonoBehaviour
{
    public bool IsOpen { get; protected set; }

    [SerializeField]
    protected Image blackFadeScreen;

    protected Animator _animator;

    public virtual void Open()
    {
        if (IsOpen)
            return;

        blackFadeScreen.enabled = true;

        IsOpen = true;

        _animator.SetTrigger("Open");
        
    }

    public virtual void Close()
    {
        if (!IsOpen)
            return;

        IsOpen = false;

        _animator.SetTrigger("Close");
        if(Time.timeScale > 0)
            StartCoroutine(COWaitForAction(1.0f, () => blackFadeScreen.enabled = false));
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
        blackFadeScreen.enabled = false;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected IEnumerator COWaitForAction(float delay, Action ActionToPerform)
    {
        yield return new WaitForSeconds(delay);
        ActionToPerform?.Invoke();
    }
}

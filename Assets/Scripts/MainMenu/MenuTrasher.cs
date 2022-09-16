using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTrasher : MonoBehaviour
{
    public bool IsMoving { get; private set; }

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float startDestination = -7.0f;

    private Animator _animator;
    private SpriteRenderer[] _spriteRenderers;

    private Vector3 _destination;
    private Vector3 _direction;

    private SkinAttachPoint _skinAttachPoint;

    private Coroutine _movementCoroutine;

    public void MoveToPoint(float xValue, Action OnMovementComplete)
    {
        IsMoving = true;
        _destination = new Vector3(xValue, transform.position.y, transform.position.z);

        if (_destination.x >= transform.position.x)
        {
            _direction = Vector3.right;
            foreach(SpriteRenderer spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            _direction = Vector3.left;
            foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.flipX = true;
                _skinAttachPoint.FlipSkin();
            }

        }
        _animator.SetBool("IsMoving", IsMoving);
        if (_movementCoroutine != null)
        {
            StopAllCoroutines();
        }
        _movementCoroutine = StartCoroutine(COMove(OnMovementComplete));
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _skinAttachPoint = GetComponentInChildren<SkinAttachPoint>();
    }

    private void Start()
    {
        Physics2D.baumgarteScale = 0.2f;
        // TO-DO: Check if first time from Player Settings
        MoveToPoint(startDestination, () => { });
    }

    private IEnumerator COMove(Action OnMovementComplete)
    {
        while (IsMoving)
        {
            transform.position += Time.deltaTime * moveSpeed * _direction;
            if ((transform.position.x >= _destination.x) && _direction == Vector3.right)
            {
                IsMoving = false;
                _animator.SetBool("IsMoving", IsMoving);

            }
            else if ((transform.position.x <= _destination.x) && _direction == Vector3.left)
            {
                IsMoving = false;
                _animator.SetBool("IsMoving", IsMoving);

            }
            yield return null;
        }
        OnMovementComplete?.Invoke();
        _movementCoroutine = null;
    }
}

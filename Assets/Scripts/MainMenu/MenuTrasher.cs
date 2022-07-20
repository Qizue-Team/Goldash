using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTrasher : MonoBehaviour
{
    public bool IsMoving { get; private set; }

    [SerializeField]
    private float moveSpeed;

    private Animator _animator;
    private SpriteRenderer[] _spriteRenderers;

    private Vector3 _destination;
    private Vector3 _direction;

    public void MoveToPoint(float xValue)
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
            }

        }
        _animator.SetBool("IsMoving", IsMoving);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (IsMoving)
        {
            transform.position += Time.deltaTime * moveSpeed * _direction;
            if((transform.position.x >= _destination.x) && _direction == Vector3.right)
            {
                IsMoving = false;
                _animator.SetBool("IsMoving", IsMoving);
            }
            else if((transform.position.x <= _destination.x) && _direction == Vector3.left)
            {
                IsMoving = false;
                _animator.SetBool("IsMoving", IsMoving);
            }
        }
    }
}

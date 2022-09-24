using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPoint : MonoBehaviour
{
    public bool IsMoving { get; private set; }

    [SerializeField]
    private float moveSpeed = 3.0f;

    private Vector3 _destination;
    private Vector3 _direction;

    public void MoveToXValue(float xValue, Action OnMoveFinished)
    {
        StopAllCoroutines();
        IsMoving = true;
        _destination = new Vector3(xValue,transform.position.y, transform.position.z);
        if(xValue >= transform.position.x)
        {
            _direction = Vector3.right;
        }
        else if (xValue < transform.position.x)
        {
            _direction = Vector3.left;
        }
        StartCoroutine(COMove(OnMoveFinished));
    }

    public void MoveToYValue(float yValue, Action OnMoveFinished)
    {
        StopAllCoroutines();
        IsMoving = true;
        _destination = new Vector3(transform.position.x, yValue, transform.position.z);
        if (yValue >= transform.position.y)
        {
            _direction = Vector3.up;
        }
        else if (yValue < transform.position.y)
        {
            _direction = Vector3.down;
        }
        StartCoroutine(COMove(OnMoveFinished));
    }

    private IEnumerator COMove(Action OnMoveFinished)
    {
        while (IsMoving)
        {
            transform.position += Time.deltaTime * _direction * moveSpeed;
            if ((transform.position.x >= _destination.x) && _direction == Vector3.right)
            {
                IsMoving = false;
            }
            else if ((transform.position.x <= _destination.x) && _direction == Vector3.left)
            {
                IsMoving = false;
            }
            else if ((transform.position.y >= _destination.y) && _direction == Vector3.up)
            {
                IsMoving = false;
            }
            else if ((transform.position.y <= _destination.y) && _direction == Vector3.down)
            {
                IsMoving = false;
            }
            yield return null;
        }
        OnMoveFinished?.Invoke();
    }
}

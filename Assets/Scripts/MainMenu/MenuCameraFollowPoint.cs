using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraFollowPoint : MonoBehaviour
{
    public delegate void OnMoveFinished();
    public static OnMoveFinished onMoveFinished;

    public bool IsMoving { get; private set; }

    [SerializeField]
    private float moveSpeed = 3.0f;

    private Vector3 _destination;
    private Vector3 _direction;

    public void MoveToXValue(float xValue)
    {
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
    }

    public void MoveToYValue(float yValue)
    {
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
    }

    private void Update()
    {
        if (IsMoving)
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
            else if((transform.position.y >= _destination.y) && _direction == Vector3.up)
            {
                IsMoving = false;
            }
            else if((transform.position.y <= _destination.y) && _direction == Vector3.down)
            {
                IsMoving = false;
            }
            if (!IsMoving)
            {
                onMoveFinished?.Invoke();
            }
        }
    }
}

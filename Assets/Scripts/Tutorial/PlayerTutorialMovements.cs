using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorialMovements : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3.0f;

    private Vector3 _destination;
    private bool _isMoving;

    public void MoveToPoint(float xValue)
    {
        if (xValue <= transform.position.x)
            return;

        _destination = new Vector3(xValue, transform.position.y, transform.position.z);
        _isMoving = true;
    }

    private void Update()
    {
        if (_isMoving)
        {
            transform.position += Time.deltaTime * Vector3.right * moveSpeed;
            if(transform.position.x >= _destination.x)
            {
                _isMoving = false;
            }
        }
    }
}

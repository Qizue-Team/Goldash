using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightEnemy : Enemy
{
    [Header("LeftRight Settings")]
    [SerializeField]
    private int movementUnits;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float raycastLength;

    [Header("Extra References")]
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private LayerMask groundLayer;

    private Vector3 movingDirection;

    private float _leftMostX;
    private float _rightMostX;
    private bool _didInvert = false;

    private bool _ignoreRight = false;

    protected override void Start()
    {
        base.Start();
        movingDirection = Vector3.left;

        _leftMostX = transform.localPosition.x - movementUnits;
        _rightMostX = transform.localPosition.x + movementUnits;
        
        if (transform.parent.gameObject.tag.Equals("RightEdge"))
        {
            StartCoroutine(COWaitIgnoreRight(1.0f)); // it was 1.0 -> it's 0.5f 'cause Laura bug fix*/
        }
           
    }

    protected override void Update()
    {
        base.Update();

        if (_isDying)
            return;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, movingDirection, raycastLength);
        Vector3 raycastDirection = transform.TransformDirection(movingDirection) * raycastLength;
        Debug.DrawRay(transform.position, raycastDirection, Color.red);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Enemy enemy = null;
                if (hit.collider.gameObject.TryGetComponent(out enemy))
                {
                    if (enemy != this)
                        InvertMovingDirection();
                }
                Trash trash = null;
                if (hit.collider.gameObject.TryGetComponent(out trash))
                {
                    scorePoints += trash.ScorePoints;
                    trash.gameObject.GetComponentInParent<TerrainTile>().DestroySpawnedObject();
                }
            }
        }

        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector3.down, 0.7f, groundLayer);
        raycastDirection = transform.TransformDirection(Vector3.down) * 0.7f;
        Debug.DrawRay(transform.position, raycastDirection, Color.green);
        if (groundHit.collider == null)
        {
            InvertMovingDirection();
        }
           

        RaycastHit2D leftEdge = Physics2D.Raycast((transform.position + new Vector3(-0.0f,0.0f,0.0f)), Vector3.down, 0.7f, groundLayer);
        RaycastHit2D rightEdge = Physics2D.Raycast((transform.position + new Vector3(0.0f, 0.0f, 0.0f)), Vector3.down, 0.7f, groundLayer);
        raycastDirection = transform.TransformDirection(Vector3.down) * 0.7f;
        Debug.DrawRay(transform.position + new Vector3(-0.0f, 0.0f, 0.0f), raycastDirection, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(0.0f, 0.0f, 0.0f), raycastDirection, Color.blue);
        if (leftEdge.collider != null && leftEdge.collider.gameObject.tag.Equals("LeftEdge"))
        {
            InvertMovingDirection();
        }
            
        if (rightEdge.collider != null && rightEdge.collider.gameObject.tag.Equals("RightEdge") && !_ignoreRight)
        {
            InvertMovingDirection();
        }
        Move();
    }

    private void Move()
    {
        transform.localPosition += Time.deltaTime * movementSpeed * movingDirection;
        if (transform.localPosition.x <= _leftMostX)
        {
            InvertMovingDirection();
        }

        if (transform.localPosition.x >= _rightMostX)
        {
            InvertMovingDirection();
        }
    }

    private void InvertMovingDirection()
    {
        if (_didInvert)
            return;
        _didInvert = true;
        if(movingDirection == Vector3.left)
        {
            movingDirection = Vector3.right;
            spriteRenderer.flipX = true;
        }
        else
        {
            movingDirection = Vector3.left;
            spriteRenderer.flipX = false;
        }
        StartCoroutine(COWaitAfterInverted(1.0f));
    }

    private IEnumerator COWaitAfterInverted(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _didInvert = false;
    }

    private IEnumerator COWaitIgnoreRight(float waitTime)
    {
        _ignoreRight = true;
        yield return new WaitForSeconds(waitTime);
        _ignoreRight = false;
    }
}

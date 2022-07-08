using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    [Header("Settings")]
    [SerializeField]
    private float jumpVelocity = 5.0f;
    [SerializeField]
    private float groundRaycastLength = 0.6f;
    [SerializeField]
    private float fallMultiplier = 2.5f;
    [SerializeField]
    private LayerMask groundLayerMask;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        DrawGroundCheckRaycast();
        CheckGrounded();
        Jump();
    }

    private void Jump()
    {
        if (_rb == null || !IsGrounded)
            return;

        // (Maybe) TODO: Check platform -> if Android (Mouse0) / if Windows (Space) 
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _rb.velocity = Vector2.up * jumpVelocity;
        }

        // Falling
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit =  Physics2D.Raycast(transform.position, Vector2.down, groundRaycastLength, groundLayerMask);

        if (hit && hit.collider.gameObject.GetComponent<TerrainTile>())
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

    private void DrawGroundCheckRaycast()
    {
        Vector3 downward = transform.TransformDirection(Vector3.down) * groundRaycastLength;
        Debug.DrawRay(transform.position, downward, Color.red);
    }
}

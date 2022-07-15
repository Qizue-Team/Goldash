using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    [Header("References")]
    [SerializeField]
    private PlayerOverheat playerOverheat;

    [Header("Settings")]
    [SerializeField]
    private float jumpVelocity = 5.0f;
    [SerializeField]
    private float bounceJumpVelocity = 5.0f;
    [SerializeField]
    private float groundRaycastLength = 0.6f;
    [SerializeField]
    private float fallGravity = 1.3f;
    [SerializeField]
    private LayerMask groundLayerMask;

    private const float BOX_CAST_X_OFFSET = 0.0f;
    private const float BOX_CAST_Y_OFFSET = 0.2f;
    private const float BOX_CAST_X_SIZE = 1.0f;
    private const float BOX_CAST_Y_SIZE = 1.5f;

    private Rigidbody2D _rb;
    private bool _isHeatIncreased = false;

    public void Jump()
    {
        if (_rb == null || !IsGrounded)
        {
            _isHeatIncreased = false;
            return;
        }

        _rb.velocity = Vector2.up * jumpVelocity;
        
        if (!_isHeatIncreased)
        {
            playerOverheat.IncreaseHeat();
            _isHeatIncreased = true;
        }
    }

    public void BounceJump()
    {
        if (_rb == null)
            return;
        
        playerOverheat.DecreaseHeat();

        _rb.velocity = Vector2.up * bounceJumpVelocity;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        DrawGroundCheckRaycast();
        CheckPlatform();
        CheckGrounded();

        // (Maybe) TODO: Check platform -> if Android (Mouse0) / if Windows (Space) 
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (_rb == null)
            return;
   
        // Falling
        if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = fallGravity;
        }
        else
        {
            _rb.gravityScale = 1;
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

    private void CheckPlatform()
    {
        Collider2D hit = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f + BOX_CAST_X_OFFSET, transform.position.y - 0.5f + BOX_CAST_Y_OFFSET),
                                                new Vector2(transform.position.x - 0.5f + BOX_CAST_X_SIZE, transform.position.y - 0.5f + BOX_CAST_Y_SIZE),
                                                groundLayerMask);
        if (hit)
        {
            if (hit.gameObject.transform.position.y >= -4)
            {
                Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), hit.gameObject.GetComponent<BoxCollider2D>());
            }
        }
    }

    private void DrawGroundCheckRaycast()
    {
        Vector3 downward = transform.TransformDirection(Vector3.down) * groundRaycastLength;
        Debug.DrawRay(transform.position, downward, Color.red);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        DrawRect(new Rect(transform.position.x - 0.5f+BOX_CAST_X_OFFSET, transform.position.y - 0.5f + BOX_CAST_Y_OFFSET, BOX_CAST_X_SIZE, BOX_CAST_Y_SIZE));
    }

    void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }
}

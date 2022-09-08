using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    public bool IsGrounded { get; private set; }
    public bool IsFastFalling { get => _isFastFalling; }
    public int JumpCount { get; private set; }

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
    private float midAirFallGravity = 2.6f;
    [SerializeField]
    private LayerMask groundLayerMask;

    private Rigidbody2D _rb;

    private bool _isFastFalling = false;
    private bool _isJumpActive = true;
    private bool _isFallJumpActive = true;

    public void SetJumpActive(bool active)
    {
        _isJumpActive = active;
    }

    public void SetFallJumpActive(bool active)
    {
        _isFallJumpActive = active;
    }

    public void Jump()
    {
        if (_rb == null || !IsGrounded)
            return;

        _rb.velocity = Vector2.up * jumpVelocity;
        
        playerOverheat.IncreaseHeat();
        JumpCount++;
    }

    public void BounceJump()
    {
        if (_rb == null)
            return;

        _isFastFalling = false;
        _rb.gravityScale = fallGravity;

        playerOverheat.DecreaseHeat();

        _rb.velocity = Vector2.up * bounceJumpVelocity;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        JumpCount = 0;
    }

    private void Update()
    {
        DrawGroundCheckRaycast();
        CheckGrounded();

        foreach (Touch touch in Input.touches)
        {
            int id = touch.fingerId;
            if (EventSystem.current.IsPointerOverGameObject(id))
            {
                return;
            }
        }
        if (EventSystem.current.IsPointerOverGameObject()) 
            return;
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && !IsGrounded && _isFallJumpActive)
        {
            CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Mid Air Fall");
            _rb.gravityScale = midAirFallGravity;
            _isFastFalling = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && _isJumpActive)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (_rb == null)
            return;

        // Falling
        if (_rb.velocity.y < 0 && !_isFastFalling)
        {
            _rb.gravityScale = fallGravity;
        }
        if (IsGrounded)
        {
            _rb.gravityScale = 1;
            _isFastFalling = false;
        }
    }

    private void CheckGrounded()
    {
        if (transform.position.y < -3.8f)
            return;
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

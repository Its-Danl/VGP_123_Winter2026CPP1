using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Ground Check Settings")]
    public LayerMask groundLayer;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float groundCheckRadius = 0.02f;


    [Header("Powerup Settings")]
    private float initialJumpForce;
    public float initialPowerupDuration = 5f;
    public float powerupJumpForce = 20f;

    private float currentPowerupDuration = 0f;
    private Coroutine jumpforceCoroutine = null;

    public void JumpForceChange()
    {
        if (jumpforceCoroutine != null)
        {
            StopCoroutine(jumpforceCoroutine);
            jumpforceCoroutine = null;
            jumpForce = 10f;
        }

        jumpforceCoroutine = StartCoroutine(JumpForceChangeCoroutine());
    }

    IEnumerator JumpForceChangeCoroutine() // powerup timer
    {
        currentPowerupDuration = initialPowerupDuration + currentPowerupDuration;
        jumpForce = powerupJumpForce;
        while (currentPowerupDuration > 0f)
        {
            currentPowerupDuration -= Time.deltaTime;
            yield return null;
        }

        jumpForce = initialJumpForce;
        jumpforceCoroutine = null;
        currentPowerupDuration = 0;
    }


    private int _lives = 3; // internal value
    private int maxLives = 5;

    public int lives //C# property accessors
    {
        get => _lives;
        set 
        {
            if (value < 0)
            {
                // die
                return;
            }

            if (value > maxLives)
            {
                _lives = maxLives;
            }
            else
            {
                _lives = value;
            }

            Debug.Log("Life pickup collected:" + _lives);
        }
    }

    private Rigidbody2D _rb;
    private Collider2D _collider;
    public SpriteRenderer _sr;
    private Animator _anim;
    private GroundCheck _groundCheck;

    private bool _isGrounded = false;
    private bool _isFiring = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        _groundCheck = new GroundCheck(_collider, _rb, groundCheckRadius, groundLayer);

        initialJumpForce = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = _groundCheck.IsGrounded();

        // input handling
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool jumpInput = Input.GetButtonDown("Jump");
        bool attackInput = Input.GetButtonDown("Fire1");

        if (horizontalInput != 0) SpriteFlip(horizontalInput);

        // movement
        if (!_isFiring)
        {
            Vector2 velocity = _rb.linearVelocity;
            velocity.x = horizontalInput * moveSpeed;
            _rb.linearVelocity = velocity;
        }

        // jumping
        if (jumpInput && _isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // attacking or shooting
        if (attackInput) // Fire
        {
            _rb.linearVelocity = Vector2.zero;
            _isFiring = true;
        }

        //else if (attackInput && !_isGrounded && verticalInput > 0) // Jump Attack
        //{
        //    _anim.SetTrigger("triggerJumpAttack");
        //}


        //animation
        _anim.SetFloat("moveInput", Mathf.Abs(horizontalInput));
        _anim.SetBool("isGrounded", _isGrounded);
        _anim.SetFloat("yVel", _rb.linearVelocity.y);
        _anim.SetBool("Fire", _isFiring);
    }
    /// <summary>
    /// Sprite flipping based on horizontal input - this function should only be called when horizontal input is non-zero
    /// </summary>
    /// <param name="horizontalInput">The input received from Unity's input system</param>
    private void SpriteFlip(float horizontalInput) => _sr.flipX = (horizontalInput < 0);
    public void ResetFireAnimation()
    {
        _isFiring = false;
    }
}


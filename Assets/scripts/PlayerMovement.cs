using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // creem dues variables del rigid body i el box collider
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D coll;
    private bool doubleJump;
    // Variables Dash
    private bool canDash = true;
    private bool isDashing;
    private float dashingSpeed = 2f;
    private float dashingTime = 0.3f;
    private float dashCooldown = 1f;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 13f;
    [SerializeField] private LayerMask jumpableGround; // des de Unity podem elegir quina mascara utilitzar

    [SerializeField] private TrailRenderer tr;

    private enum MovementState { idle, running, jumping, falling, dJumping, dashing }
    //Carregquem les dues variables
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    //modifiquem la direcció i la velocitat per fer el moviment i si premem el botó de saltar i estem al terra modifiquem la velocitat y
    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        
        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump") && (IsGrounded() || doubleJump))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            doubleJump = !doubleJump;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        UpdateAnimationState();

    }
    //Comprovem si està al terra creant un collider als peus del jugador i desplçant-lo una mica cap a sota, si toca amb el colider marcat com jumpableGround retorna true
    private bool IsGrounded()
    {
        Vector2 bottomBox = new Vector2(coll.bounds.center.x, coll.bounds.center.y - coll.bounds.size.y / 2);
        Vector2 size = new Vector2(coll.bounds.size.x, .1f);
        return Physics2D.BoxCast(bottomBox, size, 0f, Vector2.down, .1f, jumpableGround);
        
    }

    private void UpdateAnimationState()
    {
        MovementState stateAnim = 0;
        if (dirX > 0f)
        {
            stateAnim = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            stateAnim = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            stateAnim = MovementState.idle;
        }
        if (rb.velocity.y > 0.1f)
        {
            if (doubleJump)
            {
                stateAnim = MovementState.jumping;
            }
            else
            {
                stateAnim = MovementState.dJumping;
            }

        }
        else if (rb.velocity.y < -0.1f)
        {
            stateAnim = MovementState.falling;
        }
        if (isDashing)
        {
            stateAnim = MovementState.dashing;
        }
        anim.SetInteger("state", (int)stateAnim);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float oGrav = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(rb.velocity.x * dashingSpeed, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = oGrav;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}

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

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 13f;
    [SerializeField] private LayerMask jumpableGround; // des de Unity podem elegir quina mascara utilitzar

    private enum MovementState { idle, running, jumping, falling }
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
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        UpdateAnimationState();

    }
    //Comprovem si està al terra creant un collider als peus del jugador i desplçant-lo una mica cap a sota, si toca amb el colider marcat com jumpableGround retorna true
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
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
            stateAnim = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            stateAnim = MovementState.falling;
        }
        anim.SetInteger("state", (int)stateAnim);
    }
}

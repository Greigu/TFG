using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    [SerializeField] private bool goLeft = false;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private LayerMask jumpableGround;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollide()) goLeft = !goLeft;
        rb.velocity = new Vector2(goLeft ? -1 : 1 * moveSpeed, rb.velocity.y);
        sprite.flipX = !goLeft;
    }
    private bool hasCollide()
    {
        if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, goLeft ? Vector2.left : Vector2.right, .1f, jumpableGround))
        {
            return true;
        } else return false;

        
    }

    public void Die()
    {
        coll.enabled = false;
        sprite.enabled = false;
        rb.Sleep();
    }

}

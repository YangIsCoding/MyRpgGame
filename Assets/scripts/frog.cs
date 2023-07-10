using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : enemiesAll
{
    public Rigidbody2D rb;
    public Transform leftPoint;
    public Transform rightPoint;
    public bool faceLeft = true;
    public float speed, jumpForce;
    public Collider2D coll;
    public LayerMask ground;
    private float xpoint, ypoint;
   // public Animator anim;

    protected override void Start()
    {
        base.Start();
        //transform.DetachChildren();
        xpoint = leftPoint.position.x;
        ypoint = rightPoint.position.x;
        Destroy(rightPoint.gameObject);
        Destroy(leftPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement();
         SwitchAnim();
    }

    void Movement() {

        if (faceLeft)
        {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jumpForce);
            }
            if (transform.position.x < xpoint)
            {
                rb.velocity = new Vector2(0, jumpForce);
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
        }
        else {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpForce);
            }
            if (transform.position.x > ypoint)
            {
                rb.velocity = new Vector2(0, jumpForce);
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }

    void SwitchAnim() {
        if (anim.GetBool("jumping")) {
            if (rb.velocity.y < 0.1f) {
                anim.SetBool("jumping" , false);
                anim.SetBool("falling" , true);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("falling")) {
            anim.SetBool("falling", false);
        }
    }

    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagle :enemiesAll
{

    public Rigidbody2D rb;
    public float topY;
    public float buttonY;
    public Collider2D coll;
    public float speed;
    public bool isUp = true;
    public Transform top, button;
    
    protected override void Start()
    {
        base.Start();
        topY = top.position.y;
        buttonY = button.position.y;
        Destroy(top.gameObject);
        Destroy(button.gameObject);

    }

   
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (isUp) {
            rb.velocity = new Vector2(rb.velocity.x , speed);
            if (transform.position.y > topY) {
                isUp = false;
            }

        }
        else {
                rb.velocity = new Vector2(rb.velocity.x , -speed);
                if (transform.position.y < buttonY) {
                    isUp = true;
                }
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public Animator anim;
    public float speed;
    public float jumpforce;
    public Transform topCheck;
    public Transform groundCheck;
    public bool isGround, isJump;
    bool jumpPressed;
    int jumpCount;
    public LayerMask ground; 
    public Collider2D coll;
    public Collider2D disColl;
    public int Cherry = 0;
    public Text cherryNumber;
    public int Gem = 0;
    public Text gemNumber;
    private bool isHurt;
    public AudioSource jumpAudio, hurtAudio, cherryAudio, gemAudio;

  
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount >0) {
            jumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle( groundCheck.position , 0.1f, ground);
        if (!isHurt){
            Movement(); 
        }
        Jump();
        Crouch();
        SwitchAnim();
    }

    void Movement() {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
       
        rb.velocity = new Vector2( horizontalMove * speed * Time.deltaTime , rb.velocity.y);
       
        anim.SetFloat("running", Mathf.Abs(horizontalMove));

        if (horizontalMove != 0) {

            transform.localScale = new Vector3( horizontalMove , 1, 1);
        }
       
    }
   
    void Jump() {
        if (isGround) {
            jumpCount = 2;
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            jumpAudio.Play();
            anim.SetBool("jumping", true);
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpforce*Time.deltaTime);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump) {
            jumpAudio.Play();
            anim.SetBool("jumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpforce*Time.deltaTime);
            jumpCount--;
            jumpPressed = false;
        }
    }

    void SwitchAnim()
    {
        anim.SetBool("idle", false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground)) {
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping")) {
            if (rb.velocity.y < 0) {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
            
        }
        else if (isHurt) {
            anim.SetBool("hurt", true);
            anim.SetFloat("running",0f);
            if (Mathf.Abs(rb.velocity.x) < 0.1f) {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground)) {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);

        }
    }
    //collectios
    private void OnTriggerEnter2D(Collider2D collision){

        if (collision.tag == "cherry") {
            cherryAudio.Play();
            Destroy(collision.gameObject);
            Cherry += 1;
            cherryNumber.text = Cherry.ToString();
        }
        if (collision.tag == "gems") {
            gemAudio.Play();
            Destroy(collision.gameObject);
            Gem += 1;
            gemNumber.text = Gem.ToString();
        }
        if (collision.tag == "limit")
        {
           GetComponent<AudioSource>().enabled = false;
            Invoke("ReStart",1);
        }

    }

    //kill enimies & hurt
    private void OnCollisionEnter2D (Collision2D collision){
        if (collision.gameObject.tag == "enemies")
        {
            enemiesAll enemy = collision.gameObject.GetComponent<enemiesAll>();
            if (anim.GetBool("falling"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, (jumpforce - 100) * Time.deltaTime);
                anim.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-8,rb.position.y);
                hurtAudio.Play();
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity=new Vector2(8, rb.position.y);
                hurtAudio.Play();
                isHurt = true;
            }
        }

    }

    void Crouch() {
        if (!Physics2D.OverlapCircle(topCheck.position,0.2f,ground)) {
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("crouching", true);
                disColl.enabled = false;
            }
            else  {
                anim.SetBool("crouching", false);
                disColl.enabled = true;
            }
        }
    }

    void ReStart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
    }


}

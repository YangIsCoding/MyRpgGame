using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemiesAll : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource deathAudio;
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
        
    }
    public void Death() //在動畫的event呼叫
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        deathAudio.Play();
        anim.SetTrigger("death");
    }


}

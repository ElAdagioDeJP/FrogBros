using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIA : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;
    Animator animator;
    public float speed = 3f;
    public Transform CuevaArribaDer;
    public Transform CuevaAbajoDer;
    public Transform CuevaArribaIzq;
    public Transform CuevaAbajoIzq;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wolf"))
        {
            speed = -speed;
            if (!animator.GetBool("Golpe"))
            {
                if (spriteRenderer.flipX == true)
                {
                    spriteRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cueva"))
        {

            if (CuevaAbajoDer != null && CuevaArribaDer != null && CuevaAbajoIzq != null && CuevaArribaIzq != null)
            {
                if (other.transform == CuevaAbajoDer)
                {
                    transform.position = CuevaArribaDer.position;
                    ChangeDirection(); 
                }


                else if (other.transform == CuevaAbajoIzq)
                {
                    transform.position = CuevaArribaIzq.position;
                    ChangeDirection(); 
                }
            }
            else
            {
                Debug.LogError("Una o más posiciones de cuevas no están asignadas en el inspector.");
            }
        }
    }
    private void ChangeDirection()
    {
        speed = -speed;
        if (!animator.GetBool("Golpe"))
        {
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
    }
}

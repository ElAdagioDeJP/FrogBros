using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSnail : MonoBehaviour
{
    public float speed = 0.3f;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb2D;
    Animator animator;

    public Transform pipeTop1Position;
    public Transform pipeBottom1Position;
    public Transform pipeTop2Position;
    public Transform pipeBottom2Position;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Apple"))
        {
            if (!animator.GetBool("Hide"))
            {
                speed = -speed;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pipe"))
        {

            if (pipeBottom1Position != null && pipeTop1Position != null && pipeBottom2Position != null && pipeTop2Position != null)
            {

                if (other.transform == pipeBottom1Position)
                {
                    transform.position = pipeTop1Position.position;
                    ChangeDirection(); 
                }

                else if (other.transform == pipeBottom2Position)
                {
                    transform.position = pipeTop2Position.position;
                    ChangeDirection(); 
                }
            }
            else
            {
                Debug.LogError("Una o más posiciones de tuberías no están asignadas en el inspector.");
            }
        }
    }

    private void ChangeDirection()
    {
        if (!animator.GetBool("Hide"))
        {
            speed = -speed;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }


}

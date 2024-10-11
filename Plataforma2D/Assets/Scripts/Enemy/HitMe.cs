using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMe : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    MoveSnail moveSnail; // Referencia a MoveSnail
    float stop = 0;
    float speed2;
    bool cagaste = false;
    public int i = 18;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveSnail = GetComponent<MoveSnail>(); // Asegúrate de que el objeto tenga ambos componentes
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && !animator.GetBool("Hide"))
        {
            collision.transform.GetComponent<Animator>().SetBool("Hit",true);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (moveSnail.spriteRenderer.flipX == false)
        {
            speed2 = -0.3f;
        }
        else
        {
            speed2 = 0.3f;
        }
        if (collision.transform.CompareTag("ActivateAnimation"))
        {
            rb.velocity = new Vector2(stop, rb.velocity.y);
            moveSnail.speed = 0.0f; // Cambia la velocidad del caracol aquí
            StartCoroutine(ActivateAnimation());
        }
    }
    public void SetHit()
    {
        if (moveSnail.spriteRenderer.flipX == false)
        {
            speed2 = -0.3f;
        }
        else
        {
            speed2 = 0.3f;
        }
        rb.velocity = new Vector2(stop, rb.velocity.y);
        moveSnail.speed = 0.0f; // Cambia la velocidad del caracol aquí
        StartCoroutine(ActivateAnimation());
    }

    IEnumerator ActivateAnimation()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Hitme", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Hitme", false);
        animator.SetBool("Hide", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            --i;
            if(i == 0)
            {
                animator.SetBool("Hide", false);
                animator.SetBool("Run", true);
                moveSnail.speed = speed2; // Puedes restaurar la velocidad después si es necesario
                break;
            }
            if(cagaste == true)
            {
                animator.SetBool("Hitshell", true);
                yield return new WaitForSeconds(1f);
                animator.SetBool("Death", true);
                Destroy(gameObject);
                break;
            }
        }
    }
    
}

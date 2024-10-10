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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveSnail = GetComponent<MoveSnail>(); // Asegúrate de que el objeto tenga ambos componentes
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
        animator.SetBool("Hitme", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Hitme", false);
        animator.SetBool("Hide", true);
        yield return new WaitForSeconds(9f);
        animator.SetBool("Hide", false);
        moveSnail.speed = speed2; // Puedes restaurar la velocidad después si es necesario
    }
}

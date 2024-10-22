using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMe : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private MoveSnail moveSnail;
    private float stop = 0f;
    private float speed2;
    private bool rage = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveSnail = GetComponent<MoveSnail>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (moveSnail == null)
        {
            Debug.LogError("Referencia 'moveSnail' es nula en OnTriggerEnter2D.");
            return;
        }

        if (moveSnail.spriteRenderer.flipX == false)
        {
            speed2 = -0.9f;
        }
        else
        {
            speed2 = 0.9f;
        }

        if (collision.transform.CompareTag("ActivateAnimation"))
        {
            if (!animator.GetBool("Hide"))
            {
                rb.velocity = new Vector2(stop, rb.velocity.y);
                moveSnail.speed = 0.0f; // Cambia la velocidad del caracol aquí
                StartCoroutine(ActivateAnimation());
            }
            else
            {
                StopCoroutine(ActivateAnimation());

                animator.SetBool("Hide", false);
                animator.SetBool("Running", true);
                if (rage == true)
                {
                    if (moveSnail.spriteRenderer.flipX == false)
                    {
                        speed2 = -0.9f;
                    }
                    else
                    {
                        speed2 = 0.9f;
                    }
                }
                else
                {
                    if (moveSnail.spriteRenderer.flipX == false)
                    {
                        speed2 = -0.4f;
                    }
                    else
                    {
                        speed2 = 0.4f;
                    }
                }
                moveSnail.speed = speed2; // Restablecer la velocidad inmediatamente
            }
        }
    }

    public void SetHit()
    {
        if (moveSnail == null)
        {
            Debug.LogError("Referencia 'moveSnail' es nula en SetHit.");
            return;
        }

        // Verifica si el caracol está oculto
        if (animator.GetBool("Hide"))
        {
            // Detener la corrutina y cambiar estados
            StopCoroutine(ActivateAnimation());
            animator.SetBool("Hide", false);
            animator.SetBool("Running", true);

            // Restablecer la velocidad según la dirección y estado de rage
            speed2 = rage ? (moveSnail.spriteRenderer.flipX ? 0.9f : -0.9f)
                          : (moveSnail.spriteRenderer.flipX ? 0.4f : -0.4f);

            moveSnail.speed = speed2; // Restablecer la velocidad inmediatamente
        }
        else
        {
            // Si no está oculto, procede con la lógica normal de SetHit
            rb.velocity = new Vector2(stop, rb.velocity.y);
            moveSnail.speed = 0.0f; // Cambia la velocidad del caracol aquí
            StartCoroutine(ActivateAnimation());
        }
    }

    private IEnumerator ActivateAnimation()
    {
        if (animator == null)
        {
            Debug.LogError("Referencia 'animator' es nula en ActivateAnimation.");
            yield break;
        }

        animator.SetBool("Running", false);
        animator.SetBool("Hitme", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Hitme", false);
        animator.SetBool("Hide", true);
        yield return new WaitForSeconds(7.5f);
        animator.SetBool("TimeRage", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("TimeRage", false);
        if (moveSnail != null && animator.GetBool("Hide") == true)
        {
            moveSnail.speed = speed2;
            animator.SetBool("Hide", false);
        }
        else
        {
            Debug.LogWarning("Referencia 'moveSnail' es nula al final de ActivateAnimation.");
        }
        animator.SetBool("Running", true);
        rage = true;
    }
}

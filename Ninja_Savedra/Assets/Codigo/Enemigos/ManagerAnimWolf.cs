using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAnimWolf : MonoBehaviour
{
    Animator animator;
    private MoveIA moveIa;
    public SpriteRenderer spriteRenderer;
    bool correr = false;
    BoxCollider2D boxCollider2D;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        moveIa = GetComponent<MoveIA>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            if (animator.GetBool("Golpe"))
            {
                StopCoroutine(AnimHurtWolf());
                animator.SetBool("Golpe", false);
                if (correr)
                {
                    animator.SetBool("Correr", true);
                    if (spriteRenderer.flipX == true)
                    {
                        moveIa.speed = -5;
                    }
                    else
                    {
                        moveIa.speed = 5;
                    }
                }
                else
                {
                    animator.SetBool("Caminar", true);
                    animator.SetBool("Golpe", false);
                    if (spriteRenderer.flipX == true)
                    {
                        moveIa.speed = -3;
                    }
                    else
                    {
                        moveIa.speed = 3;
                    }
                }
                
                
            }
            else
            {
                moveIa.speed = 0f;
                StartCoroutine(AnimHurtWolf());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (animator.GetBool("Golpe") && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(AnimaDeathWolf());
        }
    }

    IEnumerator AnimaDeathWolf()
    {
        animator.SetBool("Muerte", true);
        boxCollider2D.enabled = false;
        yield return new WaitForSeconds(1f);
        animator.SetBool("Muerte", false);
        Destroy(gameObject);
    }

    IEnumerator AnimHurtWolf()
    {
        
        animator.SetBool("Caminar", false);
        animator.SetBool("Correr", false);
        animator.SetBool("Golpe", true);
        yield return new WaitForSeconds(7f);
        animator.SetBool("Correr", true);
        correr = true;
        animator.SetBool("Golpe", false);
        if (!animator.GetBool("Caminar"))
        {
            if (spriteRenderer.flipX == true)
            {
                moveIa.speed = -5;
            }
            else
            {
                moveIa.speed = 5;
            }
        }
    }

    public void PowActivo()
    {
        if (animator == null) return;
        if (!animator.GetBool("Golpe"))
        {
            moveIa.speed = 0f;
            StartCoroutine(AnimHurtWolf());
        }
        else
        {
            StopCoroutine(AnimHurtWolf());
            animator.SetBool("Golpe", false);
            if (correr)
            {
                animator.SetBool("Correr", true);
                if (spriteRenderer.flipX == true)
                {
                    moveIa.speed = -5;
                }
                else
                {
                    moveIa.speed = 5;
                }
            }
            else
            {
                animator.SetBool("Caminar", true);
                if (spriteRenderer.flipX == true)
                {
                    moveIa.speed = -3;
                }
                else
                {
                    moveIa.speed = 3;
                }
            }

        }
    }
}

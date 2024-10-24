using System.Collections;
using UnityEngine;

public class HitMe : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private MoveSnail moveSnail;
    private float stop = 0f;
    private float speed2;
    private bool rage = false;
    public static bool isWeakened = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveSnail = GetComponent<MoveSnail>();

        if (rb == null || animator == null || moveSnail == null)
        {
            Debug.LogError("Component references are missing.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (moveSnail == null) return;

        if (rage)
        {
            speed2 = moveSnail.spriteRenderer.flipX ? 0.9f : -0.9f;
        }
        else
        {
            speed2 = moveSnail.spriteRenderer.flipX ? 0.4f : -0.4f;
        }

        if (collision.transform.CompareTag("ActivateAnimation"))
        {
            HandleAnimation();
        }
    }

    private void HandleAnimation()
    {

        if (!animator.GetBool("Hide"))
        {
            if (rage)
            {
                speed2 = moveSnail.spriteRenderer.flipX ? 0.9f : -0.9f;
            }
            else
            {
                speed2 = moveSnail.spriteRenderer.flipX ? 0.4f : -0.4f;
            }
            rb.velocity = new Vector2(stop, rb.velocity.y);
            moveSnail.speed = 0.0f;
            StartCoroutine(ActivateAnimation());
        }
        else
        {
            StopAllCoroutines();
            animator.SetBool("Hide", false);
            animator.SetBool("Running", true);
            if (rage)
            {
                speed2 = moveSnail.spriteRenderer.flipX ? 0.9f : -0.9f;
            }
            else
            {
                speed2 = moveSnail.spriteRenderer.flipX ? 0.4f : -0.4f;
            }
            moveSnail.speed = speed2;
        }
    }


    public void SetHit()
    {
        if (animator == null) return;

        if (animator.GetBool("Hide"))
        {
            StopCoroutine(ActivateAnimation());
            animator.SetBool("Hide", false);
            animator.SetBool("Running", true);
            if (rage)
            {
                speed2 = moveSnail.spriteRenderer.flipX ? 0.9f : -0.9f;
            }
            else
            {
                speed2 = moveSnail.spriteRenderer.flipX ? 0.4f : -0.4f;
            }
            moveSnail.speed = speed2;
        }
        else
        {
            rb.velocity = new Vector2(stop, rb.velocity.y);
            moveSnail.speed = 0.0f;
            StartCoroutine(ActivateAnimation());
            Debug.Log("Entered SetHit debug.");
        }
    }

    private IEnumerator ActivateAnimation()
    {
        if (animator == null)
        {
            Debug.LogError("Animator reference is null in ActivateAnimation.");
            yield break;
        }

        animator.SetBool("Running", false);
        animator.SetBool("Hitme", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Hitme", false);
        animator.SetBool("Hide", true);
        isWeakened = true;
        yield return new WaitForSeconds(7.5f);
        animator.SetBool("TimeRage", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("TimeRage", false);

        animator.SetBool("Running", true);
        rage = true;
        if (animator.GetBool("Hide"))
        {
            if (rage)
            {
                speed2 = moveSnail.spriteRenderer.flipX ? 0.9f : -0.9f;
            }
            else
            {
                speed2 = moveSnail.spriteRenderer.flipX ? 0.4f : -0.4f;
            }
            moveSnail.speed = speed2;
            isWeakened = false;
            animator.SetBool("Hide", false);
        }
        else
        {
            Debug.LogWarning("MoveSnail reference is null at the end of ActivateAnimation.");
        }
        Debug.Log("Exited animation coroutine.");
    }
}
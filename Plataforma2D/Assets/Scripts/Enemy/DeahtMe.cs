using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeahtMe : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (animator.GetBool("Hide") == true)
            {
                StartCoroutine(AnimationDeath());
                
            }
        }
    }
   IEnumerator AnimationDeath()
    {
        animator.SetBool("Hitshell", true);
        yield return new WaitForSeconds(0.001f);
        animator.SetBool("Hide", false);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Hitshell", false);
        animator.SetBool("Dropshell", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

    }
}

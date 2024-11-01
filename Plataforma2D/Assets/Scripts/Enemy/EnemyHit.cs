using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{

    Animator animator;
    CapsuleCollider2D capsuleCollider;
    SpriteRenderer spriteRenderer;
    public GameObject Frog;
    public GameObject Enemys;
    public GameObject Win;
    public GameObject MusicWin;
    public GameObject musicBack;
    private MoveSnail moveSnail;
    private void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found on the GameObject.");
        }
    }
    private void Awake()
    {
        moveSnail = GetComponent<MoveSnail>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (animator.GetBool("Hide"))
            {
                capsuleCollider.enabled = false;
                StartCoroutine(DeathAnimation());
            }
        }
    }

    IEnumerator DeathAnimation()
    {
        float duration = 1f; 
        float time = 0f;
        float randon = Random.Range(-5, 5); 
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = new Vector2(transform.position.x*randon, -2f); 

        float startRotation = 0f;
        float targetRotation = -90f; 


        animator.SetBool("Hit", true);


        while (time < duration)
        {

            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);


            float rotation = Mathf.Lerp(startRotation, targetRotation, time / duration);
            transform.rotation = Quaternion.Euler(0, 0, rotation);

            time += Time.deltaTime;
            yield return null;
        }


        transform.position = targetPosition;
        transform.rotation = Quaternion.Euler(0, 0, targetRotation);
        Destroy(gameObject);

        if (Enemys.transform.childCount == 1)
        {
            musicBack.GetComponent<AudioSource>().Stop();
            MusicWin.GetComponent<AudioSource>().Play();
            Win.SetActive(true);
            Enemys.SetActive(false);
            Destroy(Frog);
        }
        if(Enemys.transform.childCount == 2)
        {
            
            animator.SetBool("OnlyOne", true);
            animator.SetBool("TimeRage", true);
            if (spriteRenderer.flipX == false)
            {
                moveSnail.speed = -0.9f;
            }
            else
            {
                moveSnail.speed = 0.9f;
            }
           
        }
    }
        
    
}

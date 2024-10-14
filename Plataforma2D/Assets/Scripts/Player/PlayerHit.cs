using System.Collections;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public AudioSource death;
    Animator animator;
    Vector2 respawnPoint = new Vector2(-0.3f, 1.3f);
    public GameObject lifes;
    public GameObject musicBack;
    public GameObject CheckGround;
    public GameObject CheckRoof;
    public GameObject respawn;
    public GameObject GameOver;
    AudioClip clip;
    private Vector2 startPosition;
    public Vector2 targetPosition = new Vector2(0, -2f);
    public float targetRotation = 360f;
    BoxCollider2D boxCollider2D;
    public GameObject snailObject;
    public GameObject AudioGameOver;
    HitMe hide;
    int j = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
        hide = snailObject.GetComponent<HitMe>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the GameObject.");
        }
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !hide.GetComponent<Animator>().GetBool("Hide"))
        {
            death.Play();
            StartCoroutine(AnimationDeath());
        }
    }

    IEnumerator AnimationDeath()
    {
        float duration = 3f;
        float time = 0f;

        startPosition = transform.position;
        targetPosition = new Vector2(transform.position.x, -2f);
        musicBack.GetComponent<AudioSource>().Pause();
        boxCollider2D.enabled = false;
        CheckGround.SetActive(false);
        CheckRoof.SetActive(false);
        animator.SetBool("Hit", true);

        while (time < duration)
        {

            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);


            float rotation = Mathf.Lerp(0, targetRotation, time / duration);
            transform.rotation = Quaternion.Euler(0, 0, rotation);

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = Quaternion.Euler(0, 0, targetRotation);
        respawn.SetActive(true);
        transform.position = respawnPoint;

        animator.SetBool("Hit", false);
        CheckGround.SetActive(true);
        CheckRoof.SetActive(true);
        boxCollider2D.enabled = true;


        if (lifes.transform.childCount > 0)
        {
            Destroy(lifes.transform.GetChild(0).gameObject);
        }
        else
        {
            GameOver.SetActive(true);
            respawn.SetActive(false);
            Destroy(gameObject);
            Destroy(snailObject);
            j = 1;
        }
        


        death.Stop();
        musicBack.GetComponent<AudioSource>().Play();
        if (j == 1)
        {
            musicBack.GetComponent<AudioSource>().Stop();
            AudioGameOver.GetComponent<AudioSource>().Play();
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BlockPower : MonoBehaviour
{
    public AudioSource clip;
    bool bouncing;
    public GameObject TileMap;
    public int i = 0;
    SpriteRenderer spriteRenderer;
    BoxCollider2D box;
    public GameObject snailObject1;
    public GameObject snailObject2;
    public GameObject snailObject3;
    public GameObject PipeFather;
    BouncingPipe pipe;
    HitMe hitme1;
    HitMe hitme2;
    HitMe hitme3;
    public void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        pipe = PipeFather.GetComponent<BouncingPipe>();
        if (snailObject1 != null || snailObject2 != null || snailObject3 != null)
        {
            hitme1 = snailObject1.GetComponent<HitMe>();
            hitme2 = snailObject2.GetComponent<HitMe>();
            hitme3 = snailObject3.GetComponent<HitMe>();
        }
        else
        {
            Debug.LogError("El objeto del caracol no está asignado.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HeadFrog"))
        {
            
            Bounce();
            StartCoroutine(MapAnimation());
            ChangeBlock();
            hitme1.SetHit();
            hitme2.SetHit();
            hitme3.SetHit();
            pipe.BouncePipe();

        }
    }

    void Bounce()
    {
        if (!bouncing)
        {

            i += 1;
            StartCoroutine(BounceAnimation());
        }
    }

    IEnumerator BounceAnimation()
    {
        bouncing = true;
        float time = 0;
        float duration = 0.1f;
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = (Vector2)transform.position + Vector2.up * 0.1f;

        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        time = 0;
        while (time < duration)
        {
            transform.position = Vector2.Lerp(targetPosition, startPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = startPosition;
        bouncing = false;
    }

    void ChangeBlock()
    {
        if (i == 1)
        {
            transform.localScale = new Vector3(0.03f, 0.02f, 1f);
        }
        else if (i == 2)
        {
            transform.localScale = new Vector3(0.03f, 0.01f, 1f);
        }
        else if (i > 2)
        {
            StopCoroutine(MapAnimation());
        }
        clip.Play();
    }
    IEnumerator MapAnimation()
    {
        
        TileMap.GetComponent<Animator>().SetBool("Pow", true);
        yield return new WaitForSeconds(0.2f);
        TileMap.GetComponent<Animator>().SetBool("Pow", false);
        if (i > 2)
        {
            Destroy(gameObject);
        }
        
    }
}

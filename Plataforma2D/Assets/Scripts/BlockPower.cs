using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BlockPower : MonoBehaviour
{
    public AudioSource clip;
    bool bouncing;
    public GameObject HightPow;
    public GameObject MidPow;
    public GameObject LitPow;
    public GameObject TileMap;
    public int i = 0;
    public GameObject snailObject1;
    public GameObject snailObject2;
    public GameObject snailObject3;
    HitMe hitme1;
    HitMe hitme2;
    HitMe hitme3;
    public void Awake()
    {
        
        if (snailObject1 != null || snailObject2 != null || snailObject3 != null)
        {
            hitme1 = snailObject1.GetComponent<HitMe>();
            hitme2 = snailObject2.GetComponent<HitMe>();
            hitme3 = snailObject3.GetComponent<HitMe>();
        }
        else
        {
            Debug.LogError("El objeto del caracol no est� asignado.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HeadFrog"))
        {
            clip.Play();
            Bounce();
            ChangeBlock();
            StartCoroutine(MapAnimation());
            
            hitme1.SetHit();
            hitme2.SetHit();
            hitme3.SetHit();
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

            HightPow.GetComponent<SpriteRenderer>().enabled = false;
            HightPow.GetComponent<Collider2D>().enabled = false;
            MidPow.GetComponent<SpriteRenderer>().enabled = true;
            MidPow.GetComponent<Collider2D>().enabled = true;
        }
        else if (i == 2)
        {

            MidPow.GetComponent<SpriteRenderer>().enabled = false;
            MidPow.GetComponent<Collider2D>().enabled = false;
            LitPow.GetComponent<SpriteRenderer>().enabled = true;
            LitPow.GetComponent<Collider2D>().enabled = true;
        }
        else if (i > 2)
        {
            LitPow.GetComponent<SpriteRenderer>().enabled = false;
            LitPow.GetComponent<Collider2D>().enabled = false;
        }
    }
    IEnumerator MapAnimation()
    {
        TileMap.GetComponent<Animator>().SetBool("Pow", true);
        yield return new WaitForSeconds(0.2f);
        TileMap.GetComponent<Animator>().SetBool("Pow", false);
    }
}

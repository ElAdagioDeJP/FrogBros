using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSaw : MonoBehaviour
{
    BoxCollider2D boxCollider2D;
    public PlayerHit Frog;
    Rigidbody2D rb2D;


    public float speed = 1f;           
    public float amplitude = 1f; 
    public float frequency = 1f;       
    public float duration = 5f;
    public bool Aparecer = false;

    private float time;               
    private Vector2 startPosition;     
    private bool isMoving = false;     

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();  
        startPosition = transform.position;  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Frog.SendDeath();
        }
    }

    private void FixedUpdate()
    {
        if (!isMoving) return;

        time += Time.fixedDeltaTime;

        if (time > duration)
        {
            transform.position = startPosition;
            gameObject.SetActive(false);
        }
        else
        {

            float sinY = Mathf.Sin(time * frequency * Mathf.PI * 2f) * amplitude;
            rb2D.velocity = new Vector2(speed * Random.Range(1,3), sinY);
        }
    }

    public void StartMovement()
    {
        time = 0f;                       
        transform.position = startPosition;
        isMoving = true;                      
    }

}


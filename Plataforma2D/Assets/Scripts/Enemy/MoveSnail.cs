using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSnail : MonoBehaviour
{
    public float speed = 0.3f;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb2D;

    // Referencias a las posiciones de los pares de tuber�as
    public Transform pipeTop1Position;
    public Transform pipeBottom1Position;
    public Transform pipeTop2Position;
    public Transform pipeBottom2Position;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Apple"))
        {
            speed = -speed;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    // Detectar si el caracol pasa por alguna tuber�a
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pipe"))
        {
            // Verificar que las referencias a las tuber�as no sean null
            if (pipeBottom1Position != null && pipeTop1Position != null && pipeBottom2Position != null && pipeTop2Position != null)
            {
                // Solo permitir que el caracol pase de las tuber�as inferiores hacia las superiores

                // Si el caracol toca la tuber�a inferior 1, lo teletransportamos a la tuber�a superior 1
                if (other.transform == pipeBottom1Position)
                {
                    transform.position = pipeTop1Position.position;
                    ChangeDirection(); // Invertir el movimiento al teletransportarse
                }
                // Si el caracol toca la tuber�a inferior 2, lo teletransportamos a la tuber�a superior 2
                else if (other.transform == pipeBottom2Position)
                {
                    transform.position = pipeTop2Position.position;
                    ChangeDirection(); // Invertir el movimiento al teletransportarse
                }
            }
            else
            {
                Debug.LogError("Una o m�s posiciones de tuber�as no est�n asignadas en el inspector.");
            }
        }
    }

    // Funci�n para cambiar la direcci�n del caracol
    private void ChangeDirection()
    {
        // Invertir la velocidad
        speed = -speed;

        // Invertir la direcci�n de la imagen del caracol (flipX)
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }


}

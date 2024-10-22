using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitMove : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float speed = 0.5f;
    public Transform pipeTop1Position;
    public Transform pipeBottom1Position;
    public Transform pipeTop2Position;
    public Transform pipeBottom2Position;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Apple"))
        {
            speed = -speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pipe"))
        {
            // Verificar que las referencias a las tuberías no sean null
            if (pipeBottom1Position != null && pipeTop1Position != null && pipeBottom2Position != null && pipeTop2Position != null)
            {
                // Solo permitir que el caracol pase de las tuberías inferiores hacia las superiores

                // Si el caracol toca la tubería inferior 1, lo teletransportamos a la tubería superior 1
                if (other.transform == pipeBottom1Position)
                {
                    transform.position = pipeTop1Position.position;
                    ChangeDirection(); // Invertir el movimiento al teletransportarse
                }
                // Si el caracol toca la tubería inferior 2, lo teletransportamos a la tubería superior 2
                else if (other.transform == pipeBottom2Position)
                {
                    transform.position = pipeTop2Position.position;
                    ChangeDirection(); // Invertir el movimiento al teletransportarse
                }
            }
            else
            {
                Debug.LogError("Una o más posiciones de tuberías no están asignadas en el inspector.");
            }
        }
    }
    private void ChangeDirection()
    {

      
        speed = -speed;
           

    }
}

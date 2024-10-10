
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movimiento")]
    public float runSpeed = 5f; // Aumentado para un movimiento m�s responsivo
    public float jumpForce = 12f; // Ajustado para saltos m�s precisos

    [Header("Mejoras de Salto")]
    public bool betterJump = false;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Componentes")]
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [Header("L�mites de Pantalla")]
    private Camera cam;
    private float screenLeft;
    private float screenRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        CalculateScreenBounds();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleWrapAround(); // Agregado para gestionar el wrap-around
        AnimatePlayer();
    }

    void FixedUpdate()
    {
        // Si tienes l�gica f�sica adicional, col�cala aqu�.
    }

    /// <summary>
    /// Calcula los l�mites de la pantalla en coordenadas del mundo.
    /// </summary>
    void CalculateScreenBounds()
    {
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Ajustar considerando el tama�o del sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            float spriteHalfWidth = sr.bounds.size.x / 2;
            screenLeft = cam.transform.position.x - camWidth / 2 - spriteHalfWidth;
            screenRight = cam.transform.position.x + camWidth / 2 + spriteHalfWidth;
        }
        else
        {
            // Valores por defecto si no hay SpriteRenderer
            screenLeft = cam.transform.position.x - camWidth / 2;
            screenRight = cam.transform.position.x + camWidth / 2;
        }
    }

    /// <summary>
    /// Maneja el movimiento horizontal del jugador.
    /// </summary>
    void HandleMovement()
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1f;
        }

        // Movimiento inmediato sin inercia
        rb.velocity = new Vector2(moveInput * runSpeed, rb.velocity.y);

        // Voltear el sprite seg�n la direcci�n
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    /// <summary>
    /// Maneja el salto del jugador.
    /// </summary>
    void HandleJump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && CheckGround.isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("Jump", true);
        }

        if (betterJump)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }

        // Animaci�n de salto
        if (!CheckGround.isGrounded)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }

    /// <summary>
    /// Maneja el efecto de wrap-around: reposiciona al jugador al otro lado si sale de la pantalla.
    /// </summary>
    void HandleWrapAround()
    {
        Vector3 pos = transform.position;

        if (pos.x > screenRight)
        {
            pos.x = screenLeft;
            transform.position = pos;
        }
        else if (pos.x < screenLeft)
        {
            pos.x = screenRight;
            transform.position = pos;
        }

        // Si la c�mara puede moverse, recalcula los l�mites
        // Esto puede ser movido a FixedUpdate si la c�mara cambia de posici�n continuamente
    }

    /// <summary>
    /// Actualiza las animaciones del jugador.
    /// </summary>
    void AnimatePlayer()
    {
        // Si no est� saltando, manejar la animaci�n de correr
        if (CheckGround.isGrounded)
        {
            float horizontal = Mathf.Abs(rb.velocity.x);
            animator.SetBool("Run", horizontal > 0.1f);
        }
    }

    /// <summary>
    /// Dibuja los l�mites en el editor para visualizaci�n.
    /// </summary>
    void OnDrawGizmos()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        if (cam != null)
        {
            float camHeight = 2f * cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            float left, right;
            if (sr != null)
            {
                float spriteHalfWidth = sr.bounds.size.x / 2;
                left = cam.transform.position.x - camWidth / 2 - spriteHalfWidth;
                right = cam.transform.position.x + camWidth / 2 + spriteHalfWidth;
            }
            else
            {
                left = cam.transform.position.x - camWidth / 2;
                right = cam.transform.position.x + camWidth / 2;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(left, transform.position.y - 5, 0), new Vector3(left, transform.position.y + 5, 0));
            Gizmos.DrawLine(new Vector3(right, transform.position.y - 5, 0), new Vector3(right, transform.position.y + 5, 0));
        }
    }
}


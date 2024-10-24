using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public static bool isGrounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.transform.CompareTag("Pipe") || collision.transform.CompareTag("Saw") || collision.transform.CompareTag("Enemy"))
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public static bool isGrounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Saw"))
        {
            isGrounded = false;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            
            if (HitMe.isWeakened)
            {
            }
            else
            {
                isGrounded = false;
            }
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

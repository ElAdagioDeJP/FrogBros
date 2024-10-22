using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRoof : MonoBehaviour
{
    public static bool isRoofed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            isRoofed = false;
        }
        else
        {
            if (collision.transform.CompareTag("Pipe"))
            {
                isRoofed = false;
            }
            else if (collision.transform.CompareTag("BlockPow"))
            {
                isRoofed = false;
            }
            else if (collision.transform.CompareTag("Saw"))
            {
                isRoofed = false;
            }
            else if (collision.transform.CompareTag("Apple"))
            {
                isRoofed = false;
            }
            else
            {
                isRoofed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isRoofed = false;
    }
}
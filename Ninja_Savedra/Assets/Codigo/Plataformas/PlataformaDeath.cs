using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaDeath : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
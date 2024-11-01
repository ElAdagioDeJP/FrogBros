using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarSalto : MonoBehaviour
{
    public static bool TocandoSuelo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cueva"))
        {
            TocandoSuelo = false;
        }
        else if (collision.gameObject.CompareTag("Wolf"))
        {
            TocandoSuelo = false;
        }
        else
        {
            TocandoSuelo = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        TocandoSuelo = false;
    }

}

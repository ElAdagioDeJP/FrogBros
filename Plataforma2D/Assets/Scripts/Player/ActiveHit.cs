using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveHit : MonoBehaviour
{
    BoxCollider2D boxCollider2D;
    public GameObject Barra;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

        if (Barra == null)
        {
            Debug.LogError("Barra is not assigned in the Inspector.");
        }
    }

    void FixedUpdate()
    {
        if (Barra == null) return;

        if (CheckRoof.isRoofed)
        {
            boxCollider2D.enabled = true;
            Barra.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            boxCollider2D.enabled = false;
            Barra.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
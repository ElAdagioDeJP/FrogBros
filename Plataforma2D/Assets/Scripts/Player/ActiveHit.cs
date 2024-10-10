using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveHit : MonoBehaviour
{
    BoxCollider2D boxCollider2D;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if (CheckRoof.isRoofed)
        {
            boxCollider2D.enabled = true;
        }
        else
        {
            boxCollider2D.enabled = false;
        }
    }
}

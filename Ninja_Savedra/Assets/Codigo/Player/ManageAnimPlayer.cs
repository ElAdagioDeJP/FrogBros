using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAnimPlayer : MonoBehaviour
{
    Vector2 respawnPoint = new Vector2(0.32f, 6.27f);
    public Animator animator;
    public BoxCollider2D boxCollidergrande;
    public BoxCollider2D cabeza;
    public BoxCollider2D pies;
    public BoxCollider2D cabeza2;
    public GameObject PlataformaDeath;
    public GameObject FatherVidas;
    public GameObject GameOver;
    Movimiento movimiento;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollidergrande = GetComponent<BoxCollider2D>();
        movimiento = GetComponent<Movimiento>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wolf"))
        {
            Animator wolfAnimator = collision.gameObject.GetComponent<Animator>();

            
            if (wolfAnimator != null && !wolfAnimator.GetBool("Golpe"))
            {
                StartCoroutine(AnimMuerte());
            }
        }
    }

    IEnumerator AnimMuerte()
    {
        boxCollidergrande.enabled = false;
        cabeza.enabled = false;
        cabeza2.enabled = false;
        pies.enabled = false;
        movimiento.enabled = false;
        animator.SetBool("Muriendo", true);
        yield return new WaitForSeconds(1.250f);
        if (FatherVidas.transform.childCount > 0)
        {
            Destroy(FatherVidas.transform.GetChild(0).gameObject);
        }
        else
        {
            GameOver.SetActive(true);
            Destroy(gameObject);
        }
        PlataformaDeath.SetActive(true);
        transform.position = respawnPoint;
        animator.SetBool("Muriendo", false);
        boxCollidergrande.enabled = true;
        cabeza.enabled = true;
        cabeza2.enabled = true;
        pies.enabled = true;
        movimiento.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonPow : MonoBehaviour
{
    public Animator animator;
    public Animator MoonAnimator;
    public GameObject Lobo1;
    public GameObject Lobo2;
    public GameObject Lobo3;
    ManagerAnimWolf AnimLobo1;
    ManagerAnimWolf AnimLobo2;
    ManagerAnimWolf AnimLobo3;
    int i = 0;
    ManageAnimPlayer manage;
    public static bool PowActivo = false;

    private void Awake()
    {
        AnimLobo1 = Lobo1.GetComponent<ManagerAnimWolf>();
        AnimLobo2 = Lobo2.GetComponent<ManagerAnimWolf>();
        AnimLobo3 = Lobo3.GetComponent<ManagerAnimWolf>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Powered"))
        {
            
            StartCoroutine(MoveBlock());
            StartCoroutine(MoveMap());
            ChangeMoon();
            AnimLobo1.PowActivo();
            AnimLobo2.PowActivo();
            AnimLobo3.PowActivo();


        }
    }

    void ChangeMoon()
    {
        
        i += 1;
        MoonAnimator.SetInteger("LunaCambio", i);
        
    }

    IEnumerator MoveBlock()
    {
        float time = 0;
        float duration = 0.1f;
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = (Vector2)transform.position + Vector2.up * 0.4f;

        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        time = 0;
        while (time < duration)
        {
            transform.position = Vector2.Lerp(targetPosition, startPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = startPosition;
    }
    IEnumerator MoveMap()
    {
        animator.enabled = true;
        yield return new WaitForSeconds(0.5f);
        animator.enabled = false;
        if (i > 2)
        {
            Destroy(gameObject);
        }
    }
    
}

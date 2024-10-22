using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eventsystem : MonoBehaviour
{
    public GameObject FatherSaw;
    public GameObject GameOver;
    public GameObject Win;
    public GameObject snail1;
    public GameObject snail2;
    public GameObject snail3;
    MoveSaw moveSaw;

    void Start()
    {
        StartCoroutine(StartSaw());
        StartCoroutine(StartSnail());
    }

    IEnumerator StartSaw()
    {
        yield return new WaitForSeconds(10f);
        while (true)
        {
            int i = Random.Range(0, FatherSaw.transform.childCount);
            Transform selectedChild = FatherSaw.transform.GetChild(i);
            selectedChild.gameObject.SetActive(true);

            // Obtener el MoveSaw solo si el objeto está activo
            MoveSaw moveSaw = selectedChild.GetComponent<MoveSaw>();
            if (moveSaw != null && selectedChild.gameObject.activeSelf)
            {
                moveSaw.StartMovement();
            }

            yield return new WaitForSeconds(3.5f);

            if (Win.activeSelf || GameOver.activeSelf)
            {
                break;
            }
        }
    }
    IEnumerator StartSnail()
    {
        snail1.SetActive(true);
        yield return new WaitForSeconds(5f);
        snail2.SetActive(true);
        yield return new WaitForSeconds(5f);
        snail3.SetActive(true);

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public GameObject Lobo1;
    public GameObject Lobo2;
    public GameObject Lobo3;
    public GameObject FatherLobo;
    public GameObject Victoria;
    public GameObject Samurai;
    public GameObject Punt1;
    public GameObject Punt2;
    public GameObject Punt3;

    private void FixedUpdate()
    {
        if (FatherLobo.transform.childCount == 0)
        {
            Victoria.SetActive(true);
            Punt3.SetActive(true);
            Punt2.SetActive(false);
            Destroy(Samurai);
        }
        if (FatherLobo.transform.childCount == 1)
        {
            Punt2.SetActive(true);
            Punt1.SetActive(false);
        }
        if (FatherLobo.transform.childCount == 2)
        {
            Punt1.SetActive(true);
            
        }


    }
    void Start()
    {
        StartCoroutine(SaliaLobo());
    }

    IEnumerator SaliaLobo()
    {
        yield return new WaitForSeconds(1f);
        Lobo1.SetActive(true);
        yield return new WaitForSeconds(5f);
        Lobo2.SetActive(true);
        yield return new WaitForSeconds(5f);
        Lobo3.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Eventsystem : MonoBehaviour
{
    public AudioSource Audio;
    public GameObject FatherSaw;
    public GameObject GameOver;
    public GameObject Win;
    public GameObject snail1;
    public GameObject snail2;
    public GameObject snail3;
    public GameObject FruitManager;
    public GameObject Enemys;
    public GameObject Score;
    public GameObject Apple1;
    public GameObject Apple2;
    private int currentEnemyCount;
    private int currentFruitCount;
    private int score = 0;
    private TextMeshProUGUI scoreText;
    int i = 0;
    void Start()
    {
        currentEnemyCount = Enemys.transform.childCount;
        currentFruitCount = FruitManager.transform.childCount;

        scoreText = Score.GetComponent<TextMeshProUGUI>();
        if (scoreText == null)
        {
            Debug.LogError("No se encontró un componente TextMeshProUGUI en el GameObject 'Score'. Asegúrate de que esté asignado correctamente.");
        }
        else
        {
            UpdateScoreText();
        }

        StartCoroutine(StartSaw());
        StartCoroutine(StartSnail());
    }

    void Update()
    {
        CheckForChildCountChange();
    }

    void CheckForChildCountChange()
    {
        if (Enemys == null || FruitManager == null) return;

        int newEnemyCount = Enemys.transform.childCount;
        int newFruitCount = FruitManager.transform.childCount;

        if (newEnemyCount < currentEnemyCount)
        {
            if (i == 0 && Apple1 != null)
            {
                Apple1.SetActive(true);
            }
            else if (Apple2 != null)
            {
                Apple2.SetActive(true);
            }
            int difference = currentEnemyCount - newEnemyCount;
            score += difference * 800;
            currentEnemyCount = newEnemyCount;
            i += 1;
            UpdateScoreText();
        }

        if (newFruitCount < currentFruitCount)
        {
            int difference = currentFruitCount - newFruitCount;
            score += difference * 300;
            currentFruitCount = newFruitCount;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // Actualiza el texto del marcador
    }

    // Las coroutines permanecen igual
    IEnumerator StartSaw()
    {
        yield return new WaitForSeconds(15f);
        while (true)
        {
            
            int i = Random.Range(0, FatherSaw.transform.childCount);
            Transform selectedChild = FatherSaw.transform.GetChild(i);
            selectedChild.gameObject.SetActive(true);
            MoveSaw moveSaw = selectedChild.GetComponent<MoveSaw>();
            if (moveSaw != null && selectedChild.gameObject.activeSelf)
            {
                moveSaw.StartMovement();
                
                //Audio.Play();
                
                
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
        yield return new WaitForSeconds(1f);
        snail1.SetActive(true);
        yield return new WaitForSeconds(5f);
        snail2.SetActive(true);
        yield return new WaitForSeconds(5f);
        snail3.SetActive(true);
    }
}
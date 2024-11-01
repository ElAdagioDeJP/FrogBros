using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPipe : MonoBehaviour
{
    public void BouncePipe()
    {
        StartCoroutine(BouncePipeAnimation());
    }

    IEnumerator BouncePipeAnimation()
    {
        float time = 0;
        float duration = 0.2f;
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = (Vector2)transform.position + Vector2.up * 0.06f;

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
}

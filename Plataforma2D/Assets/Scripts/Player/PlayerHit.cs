using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private float Checkx, Checky;
    public Animator animator;

    private void Start()
    {
        if(PlayerPrefs.GetFloat("Checkx") != 0)
        {
            transform.position = (new Vector2(PlayerPrefs.GetFloat("Checkx"), PlayerPrefs.GetFloat("Checky")));
        }
    }

    public void ReachedCheckPoint(float x, float y)
    {
        PlayerPrefs.GetFloat("Checkx");
        PlayerPrefs.GetFloat("Checky");
    }
    public void PlayerDamage()
    {
        Debug.Log("Entra?");
        animator.SetBool("Hit", true);
        StartCoroutine(Wait());
        animator.SetBool("Hit", false);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
    }
}

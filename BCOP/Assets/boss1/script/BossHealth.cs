using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int health = 10;
    public Slider healthBar;

    public GameObject deathEffect;

    public bool isInvulnerable = false;

    public AudioSource audioSource;
    public AudioClip hurtSound;
    public AudioClip dieSound;


    public void Damage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;
        healthBar.value = health;

        if (health <= 5)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(7);
        }
    }
}

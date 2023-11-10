using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int health = 500;
    public Slider healthBar;

    public GameObject deathEffect;

    public bool isInvulnerable = false;

    public AudioSource audioSource;
    public AudioClip hurtSound;
    public AudioClip dieSound;


    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;
        healthBar.value = health;

        if (health <= 200)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }

        if (health <= 0)
        {
            audioSource.PlayOneShot(dieSound);
            Die();
        }
    }

    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

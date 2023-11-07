using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class boss1 : MonoBehaviour
{
    public float walkTime;
    public GameObject tiro;
    public Transform firepoint;
    public bool stage;
    public float velocity;
    public int speed;

    private Rigidbody2D rig;
    private Animator anim;
    private int health;
    private float timer;
    private bool walkRight;
    private bool enraged;

    void Start()
    {
        health = 100;
        enraged = false;
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (health <= 50 && !enraged)
        {
            Enrage();
        }
    }
    
    void Enrage()
    {
        enraged = true;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= walkTime)
        {
            walkRight = !walkRight;
            timer = 0f;
        }

        if (walkRight)
        {
            transform.eulerAngles = new Vector2(0, 0);
            rig.velocity = Vector2.right * speed;
        }

        else
        {
            transform.eulerAngles = new Vector2(0, 180);
            rig.velocity = Vector2.left * speed;
        }

        void CanAttack()
        {
            if (stage == false)
            {
                anim.SetInteger("Transition", 2);
                GameObject bullet = Instantiate(tiro, firepoint.position, firepoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = firepoint.right * velocity;
                Destroy(bullet, 2f);
            }
        }

        void attack2()
        {
            if (stage == true)
            {
                IEnumerator Attack()
                {
                    anim.SetBool("Attack", true);
                    speed = 0;

                    yield return new WaitForSeconds(0.85f);
                    anim.SetBool("Attack", false);
                    speed = 1;
                }
            }
        }

    }
}

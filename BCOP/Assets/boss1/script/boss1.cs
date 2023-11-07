using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class boss1 : MonoBehaviour
{
    public float moveSpeed;
    public float attackRanger = 2.0f;
    public float attackCooldown = 2.0f;

    private int health;
    private float timer;
    private bool walkRight;
    private bool isFire;
    private bool enraged;
    private Transform playerTransform;
    private float lastAttackTime;

    private Rigidbody2D rig;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        enraged = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = Time.time;
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    private void Update()
    {
        if (health <= 50 && !enraged)
        {
            Enrage();
        }

        if (enraged)
        {
            if (CanAttack())
            {
                AttackPlayer();
            }
        }
        else
        {
            Wander();
        }

        void Enrage()
        {
            enraged = true;
        }

        void Wander()
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        void AttackPlayer()
        {

        }

        bool CanAttack()
        {
            return (Time.time - lastAttackTime) >= attackCooldown;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                int damage = 10;
                TakeDamage(damage);
            }
        }

        void TakeDamage(int damage)
        {
            health -= damage;
        }

    }
}  

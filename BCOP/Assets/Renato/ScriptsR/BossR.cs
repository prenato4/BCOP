using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossR : MonoBehaviour
{
    public float SPEED;
    public float health = 40;
    public float Mhealth = 40;
    public Image healthBare;
    public int damage;
    public Transform playerTransform;
    public float groundLevel;

    private bool isWalking;
    private bool isAttacking;

    public GameObject firePrefab; 
    public Transform fireSpawn; 
    
    public GameObject firePrefab2; 
    public Transform fireSpawn2; 

    private bool useSecondFire;
    private bool hasFired = false;


    public float attackCooldown; 
    
    private float attackTimer; 
    private Animator An;
    private Rigidbody2D RIG;

    public float minDistance;

    public AudioSource SomA;
    

    // Start is called before the first frame update
    void Start()
    {
        RIG = GetComponent<Rigidbody2D>();
        An = GetComponent<Animator>();

        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        updateHelathbaer();
    }

    private void updateHelathbaer()
    {
        healthBare.fillAmount = health / Mhealth;
    }
    
    

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = playerTransform.position - transform.position;
        direction.Normalize();
        
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance > minDistance)
        {
            RIG.velocity = new Vector2(direction.x * SPEED, RIG.velocity.y); // Mantém a velocidade vertical atual

            isWalking = direction.magnitude > 0;

            if (!isWalking)
            {
                An.SetBool("walk", false);
            }

            if (direction.x > 0)
            {
                transform.eulerAngles = new Vector2(0, 0);
                An.SetBool("walk", isWalking);
            }
            else if (direction.x < 0)
            {
                transform.eulerAngles = new Vector2(0, 180);
                An.SetBool("walk", isWalking);
            }

            
            if (attackTimer <= 0)
            {
                if (distance <= minDistance)
                {
                    Attack(); 
                    attackTimer = attackCooldown; 
                }
                else if (hasFired) 
                {
                    attackTimer = attackCooldown; 
                }
            }
            else
            {
                attackTimer -= Time.deltaTime; 
            }
        }
        else
        {
            RIG.velocity = Vector2.zero;
            An.SetBool("walk", false);
        }

        
        if (transform.position.y > groundLevel)
        {
            transform.position = new Vector3(transform.position.x, groundLevel, transform.position.z);
        }
        
    }

    public void Damage(int D)
    {
        health -= D;
        An.SetTrigger("hit");
        isAttacking = false; 

        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(4);
        }
        else if (health == 20 && !hasFired)
        {
            useSecondFire = true;
            SpawnFire();
            hasFired = true;
            
            attackCooldown = 0.5f;
            attackTimer = attackCooldown;
        }
    }


    void Attack()
    {
        isAttacking = true;
        An.SetTrigger("attack1"); 
        SomA.Play();
        FireAttack();
       
    }
    
    void FireAttack()
    {
        if (isAttacking)
        {
            SpawnFire();
            isAttacking = false;
        }
    }

    
    public void SpawnFire()
    {
        Vector2 fireDirection = playerTransform.position - fireSpawn.position;
        fireDirection.Normalize();
        
        GameObject fire;

        if (useSecondFire)
        {
            fire = Instantiate(firePrefab2, fireSpawn2.position, Quaternion.identity);
        }
        else
        {
            fire = Instantiate(firePrefab, fireSpawn.position, Quaternion.identity);
        }
        
        fire.GetComponent<atacke>().SetDirection(fireDirection);
    }


   /* private void OnCollisionEnter2D(Collision2D co)
    {
        if (co.gameObject.tag == "Player")
        {
            co.gameObject.GetComponent<Player>().Damage(damage);
        }
    }*/
}

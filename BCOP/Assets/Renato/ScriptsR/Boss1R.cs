using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss1R : MonoBehaviour
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

    private bool hasAttacked = false;

    public float attackCooldown;

    private float attackTimer;
    private Animator An;
    private Rigidbody2D RIG;

    public float minDistance;

    public AudioSource SomA;
    
    public Transform fireSpawn; 
    public float attackRange = 1.5f;



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
                else if (hasAttacked)
                {
                    attackTimer = attackCooldown;
                }
            }
            else
            {
                attackTimer -= Time.deltaTime;
                isAttacking = false; // Adicione esta linha para redefinir o valor de isAttacking
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
        else if (health == 20 && !hasAttacked)
        {
            hasAttacked = true;
            attackCooldown = 0.5f;
            attackTimer = attackCooldown;
        }
    }


    void Attack()
    {
        isAttacking = true;
        An.SetTrigger("attack1");
        SomA.Play();
        SwordAttack();
    }

    void SwordAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(fireSpawn.position, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Verifica se o inimigo possui um componente de script de dano
            Player player = enemy.GetComponent<Player>();
            if (player != null)
            {
                // Aplica o dano ao jogador
                player.Damage(damage);
            }
        }
    }



}

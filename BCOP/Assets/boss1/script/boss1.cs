using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class boss1 : MonoBehaviour
{
    public float speed;
    public float walkTime;
    public GameObject tiro;
    public Transform firepoint;
    public bool stage;
    public float vida = 20;

    public float velocity;

    private float timer;
    private bool walkRight;
    private bool isFire;

    private Rigidbody2D rig;
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    private void Update()
    {
        if (vida <= 10)
        {
            stage = true;
        }
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

        void attack()
        {
            if (stage == false)
            {
               anim.SetInteger("Transition", 2 );
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
                
            }
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossfada : MonoBehaviour
{
    public float speed;
    public float flayTime;
    public bool flayRight = true;

    public int health;
    private float timer;

    private Animator anim;
    private Rigidbody2D rig;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        
        if (timer >= flayTime)
        {
            flayRight = !flayRight;
            timer = 0f;
        }

        if (flayRight)
        {
            transform.eulerAngles = new Vector2(0, 0);
            rig.velocity = Vector2.right * speed;
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180);
            rig.velocity = Vector2.left * speed;
        }
    }

    public void Damage(int dmg)
    {
        health -= dmg; 
        anim.SetTrigger("hit");

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

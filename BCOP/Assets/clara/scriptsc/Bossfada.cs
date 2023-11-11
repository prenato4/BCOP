using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bossfada : MonoBehaviour
{
    public float speed;
    public float flayTime;
    public bool flayRight = true;
    
    private float timer;

    private Animator anim;
    private Rigidbody2D rig;
    public int health;
    public int damage;

    
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

    public void Damage(int D)
    {
        anim.SetTrigger("hit");
        health -= D;

        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(6);
        }
    }
}

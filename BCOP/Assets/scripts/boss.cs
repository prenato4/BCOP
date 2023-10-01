using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public float speed;
    public float walktime;
    private Rigidbody2D rig;
    private float timer;
    private bool walkright = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        timer = Time.deltaTime;

        if (timer >= walktime)
        {
            walkright = !walkright;
            timer = 0f;
        }

        if (walkright)
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
}

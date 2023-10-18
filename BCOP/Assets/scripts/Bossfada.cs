using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossfada : MonoBehaviour
{
    public float speed;
    public float flayTime;

    private float timer;
    private bool flayRight = true;

    private Rigidbody2D rig;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
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
}

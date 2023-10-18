using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fairyflaingattack : MonoBehaviour
{
    public float speed;
    public float flayngTime;

    private float timer;
    private bool flayngRight = true;

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

        if (timer >= flayngTime)
        {
            flayngRight = !flayngRight;
            timer = 0f;
        }

        if (flayngRight)
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
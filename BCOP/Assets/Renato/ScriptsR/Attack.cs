using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Rigidbody2D RIG;
    public float SPEED;
    public int damage;
    

    public bool Isright;
    
    // Start is called before the first frame update
    void Start()
    {
        
        RIG = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Isright)
        {
            RIG.velocity = Vector2.right * SPEED;
        }

        else
        {
            RIG.velocity = Vector2.left * SPEED;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D CO)
    {
        if (CO.gameObject.tag == "Enemy")
        {
            CO.GetComponent<BossR>().Damage(damage);
            Destroy(gameObject);
        }
        if (CO.gameObject.tag == "BOSSI")
        {
            CO.GetComponent<BossR>().Damage(damage);
            Destroy(gameObject);
        }
        if (CO.gameObject.tag == "BOSSII")
        {
            CO.GetComponent<BossR>().Damage(damage);
            Destroy(gameObject);
        }
        
        if (CO.gameObject.tag == "Enemy")
        {
            CO.GetComponent<Boss1R>().Damage(damage);
            Destroy(gameObject);
        }
        if (CO.gameObject.tag == "BOSSI")
        {
            CO.GetComponent<Boss1R>().Damage(damage);
            Destroy(gameObject);
        }
        if (CO.gameObject.tag == "BOSSII")
        {
            CO.GetComponent<Boss1R>().Damage(damage);
            Destroy(gameObject);
        }
            
        
    }
}

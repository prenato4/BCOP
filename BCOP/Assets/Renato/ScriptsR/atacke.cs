using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atacke : MonoBehaviour
{
    public float speed = 5f; 
    private Vector2 direction; 
    public int damage;
    
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(direction * speed * Time.deltaTime);

        
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D CO)
    {
        if (CO.gameObject.tag == "Player")
        {
            CO.GetComponent<Player>().Damage(damage);
            Destroy(gameObject);
        }
    }
}

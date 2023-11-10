using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class boss1 : MonoBehaviour
{
    public Transform player;

    public bool isFlipped = false;
    
    [SerializeField] public AudioSource attackSound;

    public GameObject balaProjetil;
    public Transform firepoint;
    private bool tiro;
    public float ForcaDoTiro;
    private bool flipx = false;

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    void Update()
    {
        tiro = Input.GetButtonDown("Fire1");

        Atirar();

    }

    private void Atirar()
    {
        if (tiro == true)
        {
            GameObject temp = Instantiate(balaProjetil);
            temp.transform.position = firepoint.position;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(ForcaDoTiro, 0);
            Destroy(temp.gameObject, 3f);
        }
    }
}

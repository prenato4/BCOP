using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private bool isDashing = false;
    private bool isInvulnerable = false;
    private float dashDuration = 0.5f; // Ajuste a duração do dash conforme necessário
    public float dashDistance = 2.0f; // Ajuste a distância do dash conforme necessário
    
    
    private float shieldDuration = 3.0f;
    private float shieldCooldown = 20.0f;
    private float lastShieldActivationTime = -9999.0f; // Inicializado com um valor muito negativo para garantir que o escudo possa ser ativado imediatamente
    private bool isShieldActive = false;
    private GameObject shieldObject;
    public Sprite shieldSprite;
    
    public int health = 5;
    public float speed;
    public float jumpForce;
    
    public GameObject power;
    public Transform spawn;

    private bool isfire;
    private bool IsJumPing;
    private Rigidbody2D RIG;
    private Animator AN;
    private float M;

    private bool canFire = true;

   
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        RIG = GetComponent<Rigidbody2D>();

        AN = GetComponent<Animator>();
        
        shieldObject = null;

        

    }

    // Update is called once per frame
    void Update()
    {
        Jump();

        if (!isDashing)
        {
            Move();
            AT();
            Dash();
            
            // Verificar se o botão "A" foi pressionado
            if (Input.GetKeyDown(KeyCode.A))
            {
                ToggleShield(); // Ativar ou desativar o escudo
            }
        }
        
    }
    
    void ToggleShield()
    {
        
        float currentTime = Time.time;
        
        if (!isShieldActive && (currentTime - lastShieldActivationTime >= shieldCooldown))
        {
            // Criar o escudo como um objeto GameObject com o sprite do escudo
            shieldObject = new GameObject("Shield");
            SpriteRenderer spriteRenderer = shieldObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = shieldSprite;
            spriteRenderer.sortingOrder = 1; // Certifique-se de que o escudo apareça acima do personagem
            shieldObject.transform.position = transform.position;
            shieldObject.transform.parent = transform; // Tornar o escudo filho do personagem
            isShieldActive = true;
            
            
            // Agendar o desligamento do escudo após a duração especificada
            StartCoroutine(DeactivateShield());
        
            // Atualizar o tempo da última ativação do escudo
            lastShieldActivationTime = currentTime;
        }
        else
        {
            // Desativar o escudo destruindo o objeto GameObject do escudo
            Destroy(shieldObject);
            isShieldActive = false;
        }
        
        IEnumerator DeactivateShield()
        {
            yield return new WaitForSeconds(shieldDuration);
    
            // Desativar o escudo destruindo o objeto GameObject do escudo
            Destroy(shieldObject);
            isShieldActive = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
    
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Dash para a direita
        {
            PerformDash(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.Q)) // Dash para a esquerda
        {
            PerformDash(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.W)) // Dash para cima
        {
            PerformDash(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.T)) // Dash para baixo
        {
            PerformDash(Vector2.down);
        }
    }

    void PerformDash(Vector2 dashDirection)
    {
        isDashing = true;
        isInvulnerable = true;
        

        Vector2 dashPosition = (Vector2)transform.position + dashDirection.normalized * dashDistance;

        // Teleporte o personagem para a nova posição
        RIG.MovePosition(dashPosition);

        StartCoroutine(StopDash());
    }


    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        isInvulnerable = false;
        
    }


    void Move()
    {
        M = Input.GetAxis("Horizontal");

        if (!isDashing) // Verifique se o jogador não está no estado de dash
        {
            RIG.velocity = new Vector2(M * speed, RIG.velocity.y);

            if (M > 0)
            {
                if (!IsJumPing)
                {
                    AN.SetInteger("transition", 1);
                }

                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (M < 0)
            {
                if (!IsJumPing)
                {
                    AN.SetInteger("transition", 1);
                }

                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            if (M == 0 && !IsJumPing && !isfire)
            {
                AN.SetInteger("transition", 0);
            }
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!IsJumPing)
            {
                AN.SetInteger("transition", 2);
                RIG.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                IsJumPing = true;
                
            }
        }   
    }

    void AT()
    {
        StartCoroutine("ATA");
    }

    IEnumerator ATA()
    {
        if (canFire && Input.GetKeyDown(KeyCode.F))
        {
            canFire = false;
            isfire = true;
            AN.SetInteger("transition", 3);
            
            GameObject Power = Instantiate(power, spawn.position, spawn.rotation);

            if (transform.rotation.y == 0 )
            {
                Power.GetComponent<Attack>().Isright = true;
            }

            if (transform.rotation.y == 180)
            {
                Power.GetComponent<Attack>().Isright = true;
            }
            yield return new WaitForSeconds(0.5f);
            isfire = false;
            AN.SetInteger("transition", 0);
            
                // Aguarde um tempo antes de permitir que o personagem atire novamente
                yield return new WaitForSeconds(0.3f);

            // Defina canFire como verdadeiro para permitir que o personagem atire novamente
            canFire = true;
        }
    }

    public void Damage(int DM)
    {
        if (!isShieldActive && !isInvulnerable) // Verifique se o jogador não está invulnerável
        {

            health -= DM;
            AN.SetTrigger("hit");


            if (transform.rotation.y == 0)
            {
                transform.position += new Vector3(-1f, 0, 0);
            }

            if (transform.rotation.y == 180)
            {
                transform.position += new Vector3(1f, 0, 0);
            }

            if (health <= 0)
            {
                GamerController.instance.GameOver();
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D CL)
    {
        if (CL.gameObject.layer == 6)
        {
            IsJumPing = false;
        }
        
    }
}

using System.Collections;
using UnityEngine;

public class MovimentoJogador : MonoBehaviour
{
    public float velocidade = 5f;
    public float forcaPulo = 10f;
    public float alturaAgachamento = 0.5f;
    public float forcaDoDash = 24f;
    public float tempoDoDash = 0.2f;
    public float tempoDeEsperaDoDash = 0.5f;

    private bool noChao = false;
    public bool isJumping;
    private bool chekMoviment;
    private bool flipX = false;
    private bool podeDash = true;
    private bool estaDashando = false;
    private bool facingRight = true;
    private bool lookingUp;

    public GameObject bulletPrefab;
    private float fireRate = 0.5f;
    private float nextfire;
    public Transform shootSpawner;
 
    private Rigidbody2D rb;

    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Rigidbody2D playerRb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 5f;
        rb.gravityScale = 2f;
    }

    void Update()
    {
        HandleMovimento();
        HandlePulo();
        HandleAgachamento();
        HandleDash();
        FlipSprite();
        HandleTiro();
    }

    void HandleMovimento() 
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        Vector3 movimento = new Vector3(movimentoHorizontal, 0f, 0f);
        transform.position += movimento * Time.deltaTime * velocidade;

        chekMoviment = movimentoHorizontal != 0;
    }

    void HandlePulo()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);
        }
    }

    void HandleAgachamento()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            transform.localScale = new Vector3(1f, alturaAgachamento, 1f);
        }

        if (Input.GetButtonUp("Crouch"))
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void HandleDash()
    {
        if (estaDashando || !podeDash) return;

        if (Input.GetButtonDown("Dash"))
        {
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        podeDash = false;
        estaDashando = true;
        float gravidadeOriginal = playerRb.gravityScale;
        playerRb.gravityScale = 0f;
        playerRb.velocity = new Vector2(transform.localScale.x * forcaDoDash, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(tempoDoDash);
        tr.emitting = false;
        playerRb.velocity = new Vector2(0f, playerRb.velocity.y);
        playerRb.gravityScale = gravidadeOriginal;
        estaDashando = false;
        yield return new WaitForSeconds(tempoDeEsperaDoDash);
        podeDash = true;
    }

    void FlipSprite()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        if ((flipX == false && movimentoHorizontal < 0) || (flipX == true && movimentoHorizontal > 0))
        {
            flipX = !flipX;
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }

    void HandleTiro()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextfire)
        {
            nextfire = Time.time + fireRate;
            GameObject tempBullet = Instantiate(bulletPrefab, shootSpawner.position, shootSpawner.rotation);

            // Verifica a escala X do jogador para determinar a direção do tiro
            if (transform.localScale.x < 0)
            {
                // Se a escala X for negativa, inverte a direção do tiro
                tempBullet.transform.eulerAngles = new Vector3(180, 0, 180); 
            }
        }
    }


}

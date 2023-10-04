using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float velocidade = 5f;
    private float horizontalInput;
    private bool flipX = false;
    private bool podeDash = true; // liberar dash
    private bool estaDashando = false; // dash ativo
    public float forcaDoDash = 24f; // força do dash
    public float tempoDoDash = 0.2f; // tempo do dash
    public float tempoDeEsperaDoDash = 0.5f; // recarregar o dash

    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Rigidbody2D playerRb;

    private void FixedUpdate()
    {
        // Movimento para a Direita e Esquerda
        horizontalInput = Input.GetAxis("Horizontal");
        float movimentoHorizontal = horizontalInput;
        Vector3 movimento = new Vector3(movimentoHorizontal, 0f, 0f);
        transform.position += movimento * Time.deltaTime * velocidade;

        if (estaDashando)
        {
            return;
        }

        if (Input.GetButtonDown("Dash") && podeDash)
        {
            StartCoroutine(DashCoroutine());
        }

        // Inverter sprite quando muda de direção
        if ((flipX == false && movimentoHorizontal < 0) || (flipX == true && movimentoHorizontal > 0))
        {
            Flip();
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

    private void Flip()
    {
        flipX = !flipX;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
}
